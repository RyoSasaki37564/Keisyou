using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;

public class NineKeyInputNomal : MonoBehaviour
{
    [SerializeField] Text m_dialog = default;

    [SerializeField] Transform m_poolParent;

    [SerializeField] GameObject m_effectSlash = default; //斬撃マーク
    GameObject[] m_slashs = new GameObject[9]; //斬撃のオブジェクトプール
    int m_slashIndexer = 0;

    [SerializeField] GameObject m_effectSrast = default; //刺突マーク
    GameObject[] m_srasts = new GameObject[9]; //刺突のオブジェクトプール
    int m_srastIndexer = 0;

    public float m_changeValueInterval = 1f; //値の変化速度

    [SerializeField] PostProcessVolume m_ppv;

    //[SerializeField] GameObject m_akiCutIn = default; //アキのカットインタイムライン
    //[SerializeField] List<GameObject> m_ryugekiEffectsList = default; // 各龍撃演出タイムラインを格納

    //[SerializeField] ThisOff m_akiOff = default;
    //[SerializeField] ThisOff m_RG_0Off = default;

    //[SerializeField] Animator m_enemyAnim = default;

    /*
    [SerializeField] SEPlay m_zangekiSE = default;
    [SerializeField] SEPlay m_shitotsuSE = default;
    */

    //public static float m_RG0Rate = 1.0f;

    public struct CommandCode
    {
        public int Number { get; set; }
        public int Direction { get; set; }

        /// <summary>
        /// 入力されたキーの番号と接触方向を受け取り、ID情報に変換する
        /// 1～9
        /// </summary>
        /// <param name="numID">キー番号</param>
        /// <param name="conID">接触方向</param>
        public CommandCode(int numID, int dirID)
        {
            Number = numID;
            Direction = dirID;
        }
    }
    List<CommandCode> m_commandList = new List<CommandCode>(); //ここに格納された値を参照し、該当する龍撃の演出を呼び出す。

    bool m_phase = false;

    [SerializeField] GameObject[] m_nineKeyObjs = new GameObject[9];
    Vector3[] m_nineKeyDefaultPoss = new Vector3[9]; //シェイクさせた後元に戻すため
    //シェイク関連のパラメータ
    Tweener m_tw;
    [SerializeField] float m_shakeDuration;
    [SerializeField] float m_shakeStrength;
    [SerializeField] int m_shakeVibrato;
    [SerializeField] float m_shakeRandomness;

    Vector3 m_mousePosDelta;

    bool m_isIn = false;
    bool m_isInStopper = false;

    bool m_slustFlg = false;

    bool m_zangekiFlg = false;

    float m_zangekiDirection; //斬撃角度

    const float c_originDir = 22.5f; //斬撃方向IDをとるための原角度

    GameObject m_contactNum; //現在接触している番号

    List<Collider2D> m_colls = new List<Collider2D>();

    //[SerializeField] Stop m_pr = default;

    bool m_yatteYoshi = true; //これはtrueで初期化。参加先のデリゲートの兼ね合い。

    private void Awake()
    {
        NineKeySettings();

        //m_akiCutIn.SetActive(false);
        /*
        foreach (var x in m_ryugekiEffectsList)
        {
            x.SetActive(false);
        }
        */

        //マーカーエフェクトを生成しプール
        for (var i = 0; i < 9; i++)
        {
            var x = Instantiate(m_effectSlash,  m_poolParent);
            m_slashs[i] = x;
            m_slashs[i].SetActive(false);
        }
        for (var i = 0; i < 9; i++)
        {
            var x = Instantiate(m_effectSrast, m_poolParent);
            m_srasts[i] = x;
            m_srasts[i].SetActive(false);
        }
    }

    private void OnEnable()
    {
        m_ppv.weight = 1;
        m_dialog.color = Color.black;
        foreach (var i in m_colls)
        {
            i.enabled = true;
        }

        for (var i = 0; i < m_nineKeyObjs.Length; i++)
        {
            m_nineKeyObjs[i].transform.position = m_nineKeyDefaultPoss[i];
            m_nineKeyObjs[i].transform.Find("RedLightning").gameObject.SetActive(true);
        }

        foreach(var s in m_slashs)
        {
            s.SetActive(false);
        }
        foreach(var s in m_srasts)
        {
            s.SetActive(false);
        }
        m_slashIndexer = 0;
        m_srastIndexer = 0;
    }

    private void OnDisable()
    {
        m_ppv.weight = 0;
        m_dialog.color = Color.white;
    }

    /*
    void OnEnable()
    {
        m_pr.OnPauseResume += PauseResume;
    }
    void OnDisable()
    {
        m_pr.OnPauseResume -= PauseResume;
    }
    void PauseResume(bool isPause)
    {
        if (isPause)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }
    public void Pause()
    {
        m_yatteYoshi = false;
    }
    public void Resume()
    {
        m_yatteYoshi = true;
    }
    */


    // Start is called before the first frame update
    void Start()
    {
        TouchManager.Began += (info) =>
        {
            if (m_yatteYoshi == true)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                if (hit.collider != null && hit.transform.tag == "NineKey")
                {

                    //タッチした時点でコリジョン入りしてるパターン
                    m_colls.Add(hit.collider);
                    m_contactNum = hit.collider.gameObject;
                    m_isIn = true;
                    m_isInStopper = true;
                    m_slustFlg = true;
                }
            }
        };

        TouchManager.Moved += (info) =>
        {
            if (m_yatteYoshi == true)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (hit.collider != null && hit.transform.tag == "NineKey")
                {
                    //動かしてる状態でコリジョン入りしたパターン
                    if (m_isInStopper == false)
                    {
                        m_colls.Add(hit.collider);
                        m_contactNum = hit.collider.gameObject;
                        m_isIn = true;
                        m_isInStopper = true;
                    }
                }
                else if (hit.collider == null && m_zangekiFlg == true)
                {
                    //コリジョン抜け判定
                    if (m_isInStopper == true)
                    {
                        if (m_zangekiFlg == true && m_colls[m_colls.Count - 1].enabled == true)
                        {
                            KujikenShake(m_colls[m_colls.Count - 1].gameObject);
                            //Debug.LogError("なんでやねん切りmove");
                            //m_zangekiSE.MyPlayOneShot();
                            Slash(ZangekiDirection());
                            m_colls[m_colls.Count - 1].enabled = false;
                            m_zangekiFlg = false;
                            CommandCode m_CC = new CommandCode(int.Parse(m_contactNum.name), ZangekiDirection());
                            m_commandList.Add(m_CC);
                        }

                        m_isInStopper = false;

                    }
                }
            }
        };

        TouchManager.Ended += (info) =>
        {

            if (m_yatteYoshi == true)
            {
                m_isIn = false;

                if (m_slustFlg == true && m_isInStopper == true)
                {
                    KujikenShake(m_colls[m_colls.Count - 1].gameObject);
                    //Debug.LogError("なんでやねん突きend");
                    m_srasts[m_srastIndexer].SetActive(true);
                    m_srasts[m_srastIndexer].transform.position = m_contactNum.transform.position;
                    m_srasts[m_srastIndexer].transform.SetParent(m_contactNum.transform);
                    m_contactNum.transform.GetChild(0).gameObject.SetActive(false);
                    m_srastIndexer++;
                    //m_shitotsuSE.MyPlayOneShot();
                    m_colls[m_colls.Count - 1].enabled = false;
                    m_slustFlg = false;
                    CommandCode m_CC = new CommandCode(int.Parse(m_contactNum.name), 5); //方向ID 5は中央、刺突を意味する
                    m_commandList.Add(m_CC);
                }
                else if (m_zangekiFlg == true && m_isInStopper == true)
                {
                    KujikenShake(m_colls[m_colls.Count - 1].gameObject);
                    //Debug.LogError("なんでやねん切りend");
                    Slash(ZangekiDirection());
                    //m_zangekiSE.MyPlayOneShot();
                    m_colls[m_colls.Count - 1].enabled = false;
                    m_zangekiFlg = false;
                    CommandCode m_CC = new CommandCode(int.Parse(m_contactNum.name), ZangekiDirection());
                    m_commandList.Add(m_CC);
                }

                m_isInStopper = false;
            }
        };
    }

    private void LateUpdate()
    {
        if (m_yatteYoshi == true)
        {
#if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
            if (Input.GetMouseButton(0) && m_isIn == true)
            {
                m_isIn = false;
                m_mousePosDelta = Input.mousePosition;
                m_zangekiFlg = true;
            }
#elif UNITY_ANDROID
            if (TouchManager._instance.State == TouchState.Moved && m_isIn == true)
            {
                m_mousePosDelta = TouchManager._instance._info.screenPoint;
                m_zangekiFlg = true;
            }
#endif
        }
    }

    void NineKeySettings()
    {
        for (var i = 0; i < m_nineKeyObjs.Length; i++)
        {
            m_nineKeyDefaultPoss[i] = m_nineKeyObjs[i].transform.position;
            m_nineKeyObjs[i].name = $"{i + 1}"; //1スタートでIDを入れる。名前をそのままコマンド変換する。
            m_nineKeyObjs[i].SetActive(PlayerDataAlfa.Instance.GetNineKeyActivateFlgs(i));
        }
    }

    void KujikenShake(GameObject go)
    {
        m_tw.Kill();
        go.transform.DOShakePosition(m_shakeDuration, m_shakeStrength, m_shakeVibrato, m_shakeRandomness, false, false);
    }

    int ZangekiDirection()
    {
        var heading = Input.mousePosition - m_mousePosDelta;
        var distance = heading.magnitude;
        var direction = heading / distance;
        m_zangekiDirection = Mathf.Repeat(Mathf.Atan2(direction.y, direction.x) * (180 / Mathf.PI), 360);

        //22.5　は　45 の半分。45は　360 を斬撃の方向数「8」で割った数(5 は突きとして扱う)
        
        //ゲーム画面では 九字鍵は
        // 1 2 3
        // 4 5 6
        // 7 8 9
        //　と並んでいるが、この処理部分では円形角度計算を行うため少しでもif文の可読性を上げるために
        //　7,8,9,6,3,2,1,elseで4　という風にぐるりと円形イメージで記述を行う

        if (c_originDir <= m_zangekiDirection
            && m_zangekiDirection < c_originDir + 45) 
        {
            return 7;
        }
        else if(c_originDir + 45 <= m_zangekiDirection
            && m_zangekiDirection < c_originDir + 45 * 2)
        {
            return 8;
        }
        else if (c_originDir + 45 * 2 <= m_zangekiDirection
            && m_zangekiDirection < c_originDir + 45 * 3)
        {
            return 9;
        }
        else if (c_originDir + 45 * 3 <= m_zangekiDirection
            && m_zangekiDirection < c_originDir + 45 * 4)
        {
            return 6;
        }
        else if (c_originDir + 45 * 4 <= m_zangekiDirection
            && m_zangekiDirection < c_originDir + 45 * 5)
        {
            return 3;
        }
        else if (c_originDir + 45 * 5 <= m_zangekiDirection
            && m_zangekiDirection < c_originDir + 45 * 6)
        {
            return 2;
        }
        else if (c_originDir + 45 * 6 <= m_zangekiDirection
            && m_zangekiDirection < c_originDir + 45 * 7)
        {
            return 1;
        }
        else
        {
            return 4;
        }
    }

    void Slash(int i)
    {
        m_slashs[m_slashIndexer].transform.SetParent(m_contactNum.transform);
        m_slashs[m_slashIndexer].SetActive(true);
        m_slashs[m_slashIndexer].transform.position = m_contactNum.transform.position;


        m_contactNum.transform.GetChild(0).gameObject.SetActive(false);

        //斬撃エフェクトの角度を調節
        switch (i)
        {
            case 1:
                m_slashs[m_slashIndexer].transform.rotation = new Quaternion(0, 0, 45, 90);
                break;
            case 2:
                m_slashs[m_slashIndexer].transform.rotation = new Quaternion(0, 0, 0, 90);
                break;
            case 3:
                m_slashs[m_slashIndexer].transform.rotation = new Quaternion(0, 0, -45, 90);
                break;
            case 4:
                m_slashs[m_slashIndexer].transform.rotation = new Quaternion(0, 0, 90, 90);
                break;
            case 6:
                m_slashs[m_slashIndexer].transform.rotation = new Quaternion(0, 0, -90, 90);
                break;
            case 7:
                m_slashs[m_slashIndexer].transform.rotation = new Quaternion(0, 0, 165, 90);
                break;
            case 8:
                m_slashs[m_slashIndexer].transform.rotation = new Quaternion(0, 0, 900, 70);
                break;
            case 9:
                m_slashs[m_slashIndexer].transform.rotation = new Quaternion(0, 0, -165, 90);
                break;
        }

        m_slashIndexer++;
    }

    public void Phaser()
    {
        if (m_phase == false)
        {
            m_phase = true;
            Ryuugeki(m_commandList);
            foreach (var i in m_colls)
            {
                i.enabled = true;
            }
        }
        else
        {
            m_phase = false;
            m_dialog.text = "";
            foreach (var i in m_colls)
            {
                i.enabled = true;
            }
        }
        foreach (var x in m_slashs)
        {
            x.SetActive(false);
        }
        foreach (var x in m_srasts)
        {
            x.SetActive(false);
        }
        m_slashIndexer = 0;
        m_srastIndexer = 0;
    }

    public void Ryuugeki(List<CommandCode> commands)
    {
        if (commands.Count == 0)
        {
            //m_akiOff.ThisOffMetthod();
            //m_RG_0Off.ThisOffMetthod();
            m_dialog.text = "失敗";
        }
        else
        {
            if (commands.Count == 1)
            {
                if (commands[0].Number == 2 && commands[0].Direction == 5)
                {
                    //m_RG0Rate = 1.3f;
                    //m_ryugekiEffectsList[5].SetActive(true); //現在汎用
                    m_dialog.text = " ～ 点睛 ～ ";
                }
                else
                {
                    //m_RG0Rate = 1.2f;
                    //m_ryugekiEffectsList[5].SetActive(true); //現在汎用
                    m_dialog.text = "ガチビンタ";
                }
            }
            else if (commands.Count == 2)
            {
                if (Player.Instance.m_armsMasterTable[ArmsSys.m_carsol]._type == 1 &&
                    commands[0].Number == 1 && commands[0].Direction == 1 &&
                    commands[1].Number == 9 && commands[1].Direction == 9)
                {
                    //使用武器が鳥属性の時のみ可能
                    //m_RG0Rate = 1.3f;
                    //m_ryugekiEffectsList[5].SetActive(true); //現在汎用
                    m_dialog.text = " ～ 燕返し ～ ";
                }
                else
                {
                    //m_RG0Rate = 1.2f;
                    //m_ryugekiEffectsList[5].SetActive(true); //現在汎用
                    m_dialog.text = "ガチビンタ";
                }
            }
            else if (commands.Count == 3)
            {
                if (commands[0].Number == 2 && commands[0].Direction == 2 &&
                   commands[1].Number == 5 && commands[1].Direction == 2 &&
                   commands[2].Number == 8 && commands[2].Direction == 5)
                {
                    //m_ryugekiEffectsList[0].SetActive(true);
                    m_dialog.text = " ～ 顎門落とし ～ ";
                }
                else if (commands[0].Number == 4 && commands[0].Direction == 1 &&
                        commands[1].Number == 6 && commands[1].Direction == 7 &&
                        commands[2].Number == 5 && commands[2].Direction == 5)
                {
                    //m_RG0Rate = 1.25f;
                    //m_ryugekiEffectsList[5].SetActive(true); //現在汎用
                    m_dialog.text = " ～ 徹甲突 ～ ";
                }
                else
                {
                    //m_ryugekiEffectsList[5].SetActive(true); //現在汎用
                    m_dialog.text = "ガチビンタ";
                }
            }
            else if (commands.Count == 6)
            {
                if (commands[0].Number == 7 && commands[0].Direction != 5 &&
                   commands[1].Number == 4 && commands[1].Direction != 5 &&
                   commands[2].Number == 5 && commands[2].Direction != 5 &&
                   commands[3].Number == 8 && commands[3].Direction != 5 &&
                   commands[4].Number == 9 && commands[4].Direction != 5 &&
                   commands[5].Number == 6 && commands[5].Direction != 5)
                {
                    //m_ryugekiEffectsList[1].SetActive(true);
                    m_dialog.text = " ～ 爬行連裂 ～ ";
                }
                else if (Player.Instance.m_armsMasterTable[ArmsSys.m_carsol]._type == 2 &&
                   commands[0].Number == 9 && commands[0].Direction == 6 &&
                   commands[1].Number == 4 && commands[1].Direction == 4 &&
                   commands[2].Number == 3 && commands[2].Direction == 6 &&
                   commands[3].Number == 2 &&
                   commands[4].Number == 5 &&
                   commands[5].Number == 8)
                {
                    //使用属性が風の時のみ
                    //m_RG0Rate = 1.4f;
                    //m_ryugekiEffectsList[5].SetActive(true); //現在汎用
                    m_dialog.text = " ～ 龍巻 ～ ";
                }
                else
                {
                    //m_RG0Rate = 1.2f;
                    //m_ryugekiEffectsList[5].SetActive(true); //現在汎用
                    //m_dialog.text = "ガチビンタ";
                }
            }
            else if (commands.Count == 7)
            {
                if (commands[0].Number == 1 &&
                   commands[1].Number == 2 &&
                   commands[2].Number == 3 &&
                   commands[3].Number == 5 &&
                   commands[4].Number == 7 &&
                   commands[5].Number == 8 &&
                   commands[6].Number == 6)
                {
                    //m_ryugekiEffectsList[2].SetActive(true);
                    m_dialog.text = " ～ 打尾払い ～ ";
                }
                /*
                else if(commands[0].Number == 1 && commands[0].Direction == 4 &&
                   commands[1].Number == 2 && commands[1].Direction == 4 &&
                   commands[2].Number == 3 && commands[2].Direction == 4 &&
                   commands[3].Number == 6 && commands[3].Direction == 2 &&
                   commands[4].Number == 9 && commands[4].Direction == 2 &&
                   commands[5].Number == 8 && commands[5].Direction == 6 &&
                   commands[6].Number == 7 && commands[6].Direction == 6 &&
                //月詠みの真髄解放中を示すbool &&
                //装備中の武器ID)
                {
                    m_ryugekiEffectsList[].SetActive(true);
                    m_dialog.text = " ～ 月詠み ～ ";
                }
                */
                else
                {
                    //m_RG0Rate = 1.25f;
                    //m_ryugekiEffectsList[5].SetActive(true); //現在汎用
                    m_dialog.text = "ガチビンタ";
                }
            }
            else if (commands.Count == 8)
            {
                if (commands[0].Number != 5 && commands[0].Direction != 5 &&
                    commands[1].Number != 5 && commands[1].Direction != 5 &&
                    commands[2].Number != 5 && commands[2].Direction != 5 &&
                    commands[3].Number != 5 && commands[3].Direction != 5 &&
                    commands[4].Number != 5 && commands[4].Direction != 5 &&
                    commands[5].Number != 5 && commands[5].Direction != 5 &&
                    commands[6].Number != 5 && commands[6].Direction != 5 &&
                    commands[7].Number != 5 && commands[7].Direction != 5)
                {
                    if (Player.Instance.m_armsMasterTable[ArmsSys.m_carsol]._type == 0 || Player.Instance.m_armsMasterTable[ArmsSys.m_carsol]._type == 3)
                    {
                        //すべての入力が斬撃であり、中央には不接触であり、使用している屠龍具が花か月属性である
                        //m_ryugekiEffectsList[3].SetActive(true);
                        m_dialog.text = " ～ 月下美人 ～ ";
                    }
                    else
                    {
                        //m_RG0Rate = 1.3f;
                        //m_ryugekiEffectsList[5].SetActive(true); //現在汎用
                        m_dialog.text = "ガチビンタ";
                    }

                }
                else
                {
                    //m_RG0Rate = 1.3f;
                    //m_ryugekiEffectsList[5].SetActive(true); //現在汎用
                    m_dialog.text = "ガチビンタ";
                }
            }
            else if (commands.Count == 9)
            {
                if (commands[0].Number == 1 &&
                    commands[1].Number == 2 &&
                    commands[2].Number == 3 &&
                    commands[3].Number == 6 &&
                    commands[4].Number == 9 &&
                    commands[5].Number == 8 &&
                    commands[6].Number == 7 &&
                    commands[7].Number == 4 &&
                    commands[8].Number == 5 && commands[8].Direction == 5)
                {
                    //m_RG0Rate = 1.35f;
                    //m_ryugekiEffectsList[5].SetActive(true); //現在汎用
                    m_dialog.text = " ～ とぐろ回し ～ ";
                }
                else if (commands[0].Number == 3 && commands[0].Direction == 3 &&
                    commands[1].Number == 7 && commands[1].Direction == 7 &&
                    commands[2].Number == 6 && commands[2].Direction == 6 &&
                    commands[3].Number == 4 && commands[3].Direction == 4 &&
                    commands[4].Number == 9 && commands[4].Direction == 9 &&
                    commands[5].Number == 1 && commands[5].Direction == 1 &&
                    commands[6].Number == 8 && commands[6].Direction == 8 &&
                    commands[7].Number == 2 && commands[7].Direction == 2 &&
                    commands[8].Number == 5 && commands[8].Direction == 5)
                {
                    //m_ryugekiEffectsList[4].SetActive(true);
                    m_dialog.text = " ～ カス龍閃 ～ ";
                }
                else if (commands[0].Direction == 5 &&
                    commands[1].Direction == 5 &&
                    commands[2].Direction == 5 &&
                    commands[3].Direction == 5 &&
                    commands[4].Direction == 5 &&
                    commands[5].Direction == 5 &&
                    commands[6].Direction == 5 &&
                    commands[7].Direction == 5 &&
                    commands[8].Direction == 5)
                {
                    //全部突きにすると出る
                    //m_RG0Rate = 1.5f;
                    //m_ryugekiEffectsList[5].SetActive(true); //現在汎用
                    m_dialog.text = " ～ 蜂ノ巣 ～ ";

                }
                else
                {
                    //m_RG0Rate = 1.4f;
                    //m_ryugekiEffectsList[5].SetActive(true); //現在汎用
                    m_dialog.text = "ガチビンタ9";
                }
            }
            else
            {
                //m_RG0Rate = 1f;
                //m_ryugekiEffectsList[5].SetActive(true); //現在汎用
                m_dialog.text = "ガチビンタ";
            }

            //m_akiCutIn.SetActive(true);
        }
        m_phase = false;
        commands.Clear();
    }

    void RyugekiDamage(float _iryoku, bool _isKantsu, float _interval)
    {
        EnemyStuts.m_enemiesStuts[Target.m_tergetNum].Damage(_iryoku, _isKantsu); //標的に対して通常攻撃

        //UI反映
        //Enemy.m_enemies[m_tergetIndexer.m_tergetNum].m_enemyHPSL.value = EnemyStuts.m_enemiesStuts[m_tergetIndexer.m_tergetNum].m_currentHP;
        DOTween.To(() => Enemy.m_enemies[Target.m_tergetNum].m_enemyHPSL.value, x => Enemy.m_enemies[Target.m_tergetNum].m_enemyHPSL.value = x,
            Enemy.m_enemies[Target.m_tergetNum].m_enemyHPSL.value - _iryoku, _interval);


        Debug.Log(EnemyStuts.m_enemiesStuts[Target.m_tergetNum].m_currentHP);
    }


    //各種龍撃に対応するダメージ関数は 演出用TimeLine のシングルレシーバーから呼び出してる
    public void RGAgito()
    {
        RyugekiDamage(Player.Instance.m_armsMasterTable[ArmsSys.m_carsol]._ryugeki * 1.1f, false, m_changeValueInterval);
        //m_enemyAnim.SetBool("IsDamaged", true);
    }

    public void RGTekkou()
    {
        RyugekiDamage(Player.Instance.m_armsMasterTable[ArmsSys.m_carsol]._ryugeki, true, m_changeValueInterval);
        //m_enemyAnim.SetBool("IsDamaged", true);
    }

    public void RGHakou()
    {
        //四回呼び出してね
        float rand = Random.Range(0.8f, 1.2f);
        RyugekiDamage(Player.Instance.m_armsMasterTable[ArmsSys.m_carsol]._ryugeki * rand / 4, false, m_changeValueInterval / 10);
        //m_enemyAnim.SetBool("IsDamaged", true);
    }

    int m_dabiCount = 0; //コード長いしこの関数でしか使わないメンバーのためここに書こう
    public void RGDabi()
    {
        float rand = Random.Range(0.9f, 1.2f);
        switch (m_dabiCount)
        {
            case 0:
                RyugekiDamage(Player.Instance.m_armsMasterTable[ArmsSys.m_carsol]._ryugeki * rand / 5, false, m_changeValueInterval);
                //m_enemyAnim.SetBool("IsDamaged", true);
                break;
            case 1:
                rand = 0.9f;
                RyugekiDamage(Player.Instance.m_armsMasterTable[ArmsSys.m_carsol]._ryugeki * rand / 5, false, m_changeValueInterval);
                break;
            case 2:
                rand = Random.Range(1f, 1.4f);
                RyugekiDamage(Player.Instance.m_armsMasterTable[ArmsSys.m_carsol]._ryugeki * rand, true, m_changeValueInterval);
                m_dabiCount = 0;
                break;
        }
        m_dabiCount++;
    }

    public void RG_GekkaBijin()
    {
        float rand = Random.Range(1.3f, 1.6f);
        RyugekiDamage(Player.Instance.m_armsMasterTable[ArmsSys.m_carsol]._ryugeki * rand / 5.5f, false, m_changeValueInterval * 1.5f);
        //m_enemyAnim.SetBool("IsDamaged", true);
    }

    public void RG_Kuzuryu()
    {
        float rand = Random.Range(1f, 1.5f);
        RyugekiDamage(Player.Instance.m_armsMasterTable[ArmsSys.m_carsol]._ryugeki * rand / 1.5f, true, m_changeValueInterval);
        //m_enemyAnim.SetBool("IsDamaged", true);
    }

    public void RG_0()
    {
        Debug.Log("ダメージ処理の代わりのデバッグログ");
        //RyugekiDamage(Player.Instance.m_armsMasterTable[ArmsSys.m_carsol]._ryugeki / 3.5f * m_RG0Rate, false, m_changeValueInterval);
        //m_enemyAnim.SetBool("IsDamaged", true);
    }


    public void ShinzuiDusk()
    {
        //白銀の真髄解放ダメージ
        RyugekiDamage(Player.Instance.m_armsMasterTable[4]._atk * 3, true, m_changeValueInterval);
        //m_enemyAnim.SetBool("IsDamaged", true);
    }
}
