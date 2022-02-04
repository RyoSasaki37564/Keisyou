using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NineKeyInput : MonoBehaviour
{
    [SerializeField] Text m_dialog = default;

    [SerializeField] GameObject[] m_commands = new GameObject[9];

    [SerializeField] GameObject m_effectSlash = default; //斬撃マーク
    GameObject[] m_slashs = new GameObject[9]; //斬撃のオブジェクトプール
    int m_slashIndexer = 0;

    [SerializeField] GameObject m_effectSrast = default; //刺突マーク
    GameObject[] m_srasts = new GameObject[9]; //刺突のオブジェクトプール
    int m_srastIndexer = 0;

    [SerializeField] Target m_tergetIndexer = default; //標的

    public float m_changeValueInterval = 1f; //値の変化速度

    [SerializeField] GameObject m_akiCutIn = default; //アキのカットインタイムライン
    [SerializeField] List<GameObject> m_ryugekiEffectsList = default; // 各龍撃演出タイムラインを格納

    [SerializeField] Animator m_enemyAnim = default;

    public struct CommandCode
    {
        public int Number { get; set; }
        public int Contact { get; set; }

        /// <summary>
        /// 入力されたキーの番号と接触方向を受け取り、ID情報に変換する
        /// </summary>
        /// <param name="numID">キー番号</param>
        /// <param name="conID">接触方向</param>
        public CommandCode(int numID, int conID)
        {
            Number = numID;
            Contact = conID;
        }
    }
    List<CommandCode> m_commandList = new List<CommandCode>(); //ここに格納された値を参照し、該当する龍撃の演出を呼び出す。

    bool m_phase = false;

    private void Awake()
    {
        m_akiCutIn.SetActive(false);
        foreach (var x in m_ryugekiEffectsList)
        {
            x.SetActive(false);
        }
            //マーカーエフェクトを生成しプール
            for (var i = 0; i < 9; i++)
        {
            var x = Instantiate(m_effectSlash);
            m_slashs[i] = x;
            m_slashs[i].SetActive(false);
        }
        for (var i = 0; i < 9; i++)
        {
            var x = Instantiate(m_effectSrast);
            m_srasts[i] = x;
            m_srasts[i].SetActive(false);
        }
    }

    private void Start()
    {
        TouchManager.Moved += (info) =>
        {
            if (m_phase == false)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                //キー番号とキーに対する接触情報から入力IDを生成しリストに格納
                if (hit.collider)
                {
                    //Debug.Log("接触情報：" + hit.collider.gameObject.transform.parent.name + " " + hit.collider.gameObject.name);
                    int i = int.Parse(hit.collider.gameObject.name); //接触位置
                    CommandCode m_CC = new CommandCode(int.Parse(hit.collider.gameObject.transform.parent.name), i);

                    if (i == 5) //5は刺突
                    {
                        m_srasts[m_srastIndexer].SetActive(true);
                        m_srasts[m_srastIndexer].transform.position = hit.collider.gameObject.transform.parent.position;
                        m_srastIndexer++;
                    }
                    else //それ以外は斬撃
                    {
                        m_slashs[m_slashIndexer].SetActive(true);
                        m_slashs[m_slashIndexer].transform.position = hit.collider.gameObject.transform.parent.position;
                        //斬撃の角度を調節
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

                    hit.collider.gameObject.transform.parent.gameObject.SetActive(false); //入力したキーはその龍撃中反応を切る

                    m_commandList.Add(m_CC);
                }
            }
        };

    }

    public void Phaser()
    {
        if(m_phase == false)
        {
            m_phase = true;
            Ryuugeki(m_commandList);
            foreach (var i in m_commands)
            {
                i.SetActive(true);
            }
        }
        else
        {
            m_phase = false;
            m_dialog.text = "";
            foreach (var i in m_commands)
            {
                i.SetActive(true);
            }
        }
        foreach(var x in m_slashs)
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
            m_dialog.text = "失敗";
        }
        else
        {
            if (commands.Count == 3)
            {
                if (commands[0].Number == 2 &&
                   commands[1].Number == 5 &&
                   commands[2].Number == 8 && commands[2].Contact == 5) //顎門落とし
                {
                    m_ryugekiEffectsList[0].SetActive(true);
                    m_dialog.text = "顎門落とし";
                }
                else
                {
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
                    commands[8].Number == 5 && commands[8].Contact == 5)
                {
                    m_dialog.text = "とぐろ回し";
                }
                else
                {
                    m_dialog.text = "ガチビンタ";
                }
            }
            else
            {
                m_dialog.text = "ガチビンタ";
            }

            m_akiCutIn.SetActive(true);
        }
        m_phase = false;
        commands.Clear();
    }

    void RyugekiDamage(float _iryoku, bool _isKantsu)
    {
        EnemyStuts.m_enemiesStuts[m_tergetIndexer.m_tergetNum].Damage(_iryoku, _isKantsu); //標的に対して通常攻撃

        //UI反映
        //Enemy.m_enemies[m_tergetIndexer.m_tergetNum].m_enemyHPSL.value = EnemyStuts.m_enemiesStuts[m_tergetIndexer.m_tergetNum].m_currentHP;
        DOTween.To(() => Enemy.m_enemies[m_tergetIndexer.m_tergetNum].m_enemyHPSL.value, x => Enemy.m_enemies[m_tergetIndexer.m_tergetNum].m_enemyHPSL.value = x,
            Enemy.m_enemies[m_tergetIndexer.m_tergetNum].m_enemyHPSL.value - _iryoku, m_changeValueInterval);
    }

    public void RGAgito()
    {
        RyugekiDamage(Player.Instance.m_attack * 10 * GameManager.Instance.m_enemyMaster[0].e_attack, false);
        m_enemyAnim.SetBool("IsDamaged", true);
    }
}