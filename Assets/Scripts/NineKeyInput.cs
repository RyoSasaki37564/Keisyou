﻿using System.Collections;
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

    public float m_changeValueInterval = 1f; //値の変化速度

    [SerializeField] GameObject m_akiCutIn = default; //アキのカットインタイムライン
    [SerializeField] List<GameObject> m_ryugekiEffectsList = default; // 各龍撃演出タイムラインを格納

    [SerializeField] ThisOff m_akiOff = default;
    [SerializeField] ThisOff m_RG_0Off = default;

    [SerializeField] Animator m_enemyAnim = default;

    [SerializeField] SEPlay m_zangekiSE = default;
    [SerializeField] SEPlay m_shitotsuSE = default;

    public static float m_RG0Rate = 1.0f;

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
        this.gameObject.SetActive(false);
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
                if (hit.collider && hit.transform.tag == "NineKey")
                {
                    //Debug.Log("接触情報：" + hit.collider.gameObject.transform.parent.name + " " + hit.collider.gameObject.name);
                    int i = int.Parse(hit.collider.gameObject.name); //接触位置
                    CommandCode m_CC = new CommandCode(int.Parse(hit.collider.gameObject.transform.parent.name), i);

                    if (i == 5) //5は刺突
                    {
                        m_shitotsuSE.MyPlayOneShot();
                        m_srasts[m_srastIndexer].SetActive(true);
                        m_srasts[m_srastIndexer].transform.position = hit.collider.gameObject.transform.parent.position;
                        m_srastIndexer++;
                    }
                    else //それ以外は斬撃
                    {
                        m_zangekiSE.MyPlayOneShot();
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
            m_akiOff.ThisOffMetthod();
            m_RG_0Off.ThisOffMetthod();
            m_dialog.text = "失敗";
        }
        else
        {
            if(commands.Count == 1)
            {
                if(commands[0].Number == 2 && commands[0].Contact == 5)
                {
                    m_RG0Rate = 1.3f;
                    m_ryugekiEffectsList[5].SetActive(true); //現在汎用
                    m_dialog.text = " ～ 点睛 ～ ";
                }
                else
                {
                    m_RG0Rate = 1.2f;
                    m_ryugekiEffectsList[5].SetActive(true); //現在汎用
                    m_dialog.text = "ガチビンタ";
                }
            }
            else if(commands.Count == 2)
            {
                if(Player.Instance.m_armsMasterTable[ArmsSys.m_carsol]._type == 1 &&
                    commands[0].Number == 1 && commands[0].Contact == 1 &&
                    commands[1].Number == 9 && commands[1].Contact == 9)
                {
                    //使用武器が鳥属性の時のみ可能
                    m_RG0Rate = 1.3f;
                    m_ryugekiEffectsList[5].SetActive(true); //現在汎用
                    m_dialog.text = " ～ 燕返し ～ ";
                }
                else
                {
                    m_RG0Rate = 1.2f;
                    m_ryugekiEffectsList[5].SetActive(true); //現在汎用
                    m_dialog.text = "ガチビンタ";
                }
            }
            else if (commands.Count == 3)
            {
                if (commands[0].Number == 2 &&
                   commands[1].Number == 5 &&
                   commands[2].Number == 8 && commands[2].Contact == 5)
                {
                    m_ryugekiEffectsList[0].SetActive(true);
                    m_dialog.text = " ～ 顎門落とし ～ ";
                }
                else if(commands[0].Number == 4 &&commands[0].Contact == 1 &&
                        commands[1].Number == 6 && commands[1].Contact == 7 &&
                        commands[2].Number == 5 && commands[2].Contact == 5)
                {
                    m_RG0Rate = 1.25f;
                    m_ryugekiEffectsList[5].SetActive(true); //現在汎用
                    m_dialog.text = " ～ 徹甲突 ～ ";
                }
                else
                {
                    m_ryugekiEffectsList[5].SetActive(true); //現在汎用
                    m_dialog.text = "ガチビンタ";
                }
            }
            else if(commands.Count == 6)
            {
                if(commands[0].Number == 7 && commands[0].Contact != 5 &&
                   commands[1].Number == 4 && commands[1].Contact != 5 &&
                   commands[2].Number == 5 && commands[2].Contact != 5 &&
                   commands[3].Number == 8 && commands[3].Contact != 5 &&
                   commands[4].Number == 9 && commands[4].Contact != 5 &&
                   commands[5].Number == 6 && commands[5].Contact != 5)
                {
                    m_ryugekiEffectsList[1].SetActive(true);
                    m_dialog.text = " ～ 爬行連裂 ～ ";
                }
                else if(Player.Instance.m_armsMasterTable[ArmsSys.m_carsol]._type == 2 &&
                   commands[0].Number == 9 && commands[0].Contact == 6 &&
                   commands[1].Number == 4 && commands[1].Contact == 4 &&
                   commands[2].Number == 3 && commands[2].Contact == 6 &&
                   commands[3].Number == 2 &&
                   commands[4].Number == 5 &&
                   commands[5].Number == 8)
                {
                    //使用属性が風の時のみ
                    m_RG0Rate = 1.4f;
                    m_ryugekiEffectsList[5].SetActive(true); //現在汎用
                    m_dialog.text = " ～ 龍巻 ～ ";
                }
                else
                {
                    m_RG0Rate = 1.2f;
                    m_ryugekiEffectsList[5].SetActive(true); //現在汎用
                    m_dialog.text = "ガチビンタ";
                }
            }
            else if(commands.Count == 7)
            {
                if (commands[0].Number == 1 &&
                   commands[1].Number == 2 &&
                   commands[2].Number == 3 &&
                   commands[3].Number == 5 &&
                   commands[4].Number == 7 &&
                   commands[5].Number == 8 &&
                   commands[6].Number == 6)
                {
                    m_ryugekiEffectsList[2].SetActive(true);
                    m_dialog.text = " ～ 打尾払い ～ ";
                }
                /*
                else if(commands[0].Number == 1 && commands[0].Contact == 4 &&
                   commands[1].Number == 2 && commands[1].Contact == 4 &&
                   commands[2].Number == 3 && commands[2].Contact == 4 &&
                   commands[3].Number == 6 && commands[3].Contact == 2 &&
                   commands[4].Number == 9 && commands[4].Contact == 2 &&
                   commands[5].Number == 8 && commands[5].Contact == 6 &&
                   commands[6].Number == 7 && commands[6].Contact == 6 &&
                //月詠みの真髄解放中を示すbool &&
                //装備中の武器ID)
                {
                    m_ryugekiEffectsList[].SetActive(true);
                    m_dialog.text = " ～ 月詠み ～ ";
                }
                */
                else
                {
                    m_RG0Rate = 1.25f;
                    m_ryugekiEffectsList[5].SetActive(true); //現在汎用
                    m_dialog.text = "ガチビンタ";
                }
            }
            else if(commands.Count == 8)
            {
                if (commands[0].Number != 5 && commands[0].Contact != 5 &&
                    commands[1].Number != 5 && commands[1].Contact != 5 &&
                    commands[2].Number != 5 && commands[2].Contact != 5 &&
                    commands[3].Number != 5 && commands[3].Contact != 5 &&
                    commands[4].Number != 5 && commands[4].Contact != 5 &&
                    commands[5].Number != 5 && commands[5].Contact != 5 &&
                    commands[6].Number != 5 && commands[6].Contact != 5 &&
                    commands[7].Number != 5 && commands[7].Contact != 5)
                {
                    if (Player.Instance.m_armsMasterTable[ArmsSys.m_carsol]._type == 0 || Player.Instance.m_armsMasterTable[ArmsSys.m_carsol]._type == 3)
                    {
                        //すべての入力が斬撃であり、中央には不接触であり、使用している屠龍具が花か月属性である
                        m_ryugekiEffectsList[3].SetActive(true);
                        m_dialog.text = " ～ 月下美人 ～ ";
                    }
                    else
                    {
                        m_RG0Rate = 1.3f;
                        m_ryugekiEffectsList[5].SetActive(true); //現在汎用
                        m_dialog.text = "ガチビンタ";
                    }

                }
                else
                {
                    m_RG0Rate = 1.3f;
                    m_ryugekiEffectsList[5].SetActive(true); //現在汎用
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
                    m_RG0Rate = 1.35f;
                    m_ryugekiEffectsList[5].SetActive(true); //現在汎用
                    m_dialog.text = " ～ とぐろ回し ～ ";
                }
                else if (commands[0].Number == 3 && commands[0].Contact == 3 &&
                    commands[1].Number == 7 && commands[1].Contact == 7 &&
                    commands[2].Number == 6 && commands[2].Contact == 6 &&
                    commands[3].Number == 4 && commands[3].Contact == 4 &&
                    commands[4].Number == 9 && commands[4].Contact == 9 &&
                    commands[5].Number == 1 && commands[5].Contact == 1 &&
                    commands[6].Number == 8 && commands[6].Contact == 8 &&
                    commands[7].Number == 2 && commands[7].Contact == 2 &&
                    commands[8].Number == 5 && commands[8].Contact == 5)
                {
                    m_ryugekiEffectsList[4].SetActive(true);
                    m_dialog.text = " ～ カス龍閃 ～ ";
                }
                else if(commands[0].Contact == 5 &&
                    commands[1].Contact == 5 &&
                    commands[2].Contact == 5 &&
                    commands[3].Contact == 5 &&
                    commands[4].Contact == 5 &&
                    commands[5].Contact == 5 &&
                    commands[6].Contact == 5 &&
                    commands[7].Contact == 5 &&
                    commands[8].Contact == 5)
                {
                    //全部突きにすると出る
                    m_RG0Rate = 1.5f;
                    m_ryugekiEffectsList[5].SetActive(true); //現在汎用
                    m_dialog.text = " ～ 蜂ノ巣 ～ ";

                }
                else
                {
                    m_RG0Rate = 1.4f;
                    m_ryugekiEffectsList[5].SetActive(true); //現在汎用
                    m_dialog.text = "ガチビンタ";
                }
            }
            else
            {
                m_RG0Rate = 1f;
                m_ryugekiEffectsList[5].SetActive(true); //現在汎用
                m_dialog.text = "ガチビンタ";
            }

            m_akiCutIn.SetActive(true);
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
        m_enemyAnim.SetBool("IsDamaged", true);
    }

    public void RGTekkou()
    {
        RyugekiDamage(Player.Instance.m_armsMasterTable[ArmsSys.m_carsol]._ryugeki, true, m_changeValueInterval);
        m_enemyAnim.SetBool("IsDamaged", true);
    }

    public void RGHakou()
    {
        //四回呼び出してね
        float rand = Random.Range(0.8f, 1.2f);
        RyugekiDamage(Player.Instance.m_armsMasterTable[ArmsSys.m_carsol]._ryugeki * rand / 4, false, m_changeValueInterval / 10);
        m_enemyAnim.SetBool("IsDamaged", true);
    }

    int m_dabiCount = 0; //コード長いしこの関数でしか使わないメンバーのためここに書こう
    public void RGDabi()
    {
        float rand = Random.Range(0.9f, 1.2f);
        switch (m_dabiCount)
        {
            case 0:
                RyugekiDamage(Player.Instance.m_armsMasterTable[ArmsSys.m_carsol]._ryugeki * rand / 5, false, m_changeValueInterval);
                m_enemyAnim.SetBool("IsDamaged", true);
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
        m_enemyAnim.SetBool("IsDamaged", true);
    }

    public void RG_Kuzuryu()
    {
        float rand = Random.Range(1f, 1.5f);
        RyugekiDamage(Player.Instance.m_armsMasterTable[ArmsSys.m_carsol]._ryugeki * rand / 1.5f, true, m_changeValueInterval);
        m_enemyAnim.SetBool("IsDamaged", true);
    }

    public void RG_0()
    {
        RyugekiDamage(Player.Instance.m_armsMasterTable[ArmsSys.m_carsol]._ryugeki / 3.5f * m_RG0Rate, false, m_changeValueInterval);
        m_enemyAnim.SetBool("IsDamaged", true);
    }


    public void ShinzuiDusk()
    {
        //白銀の真髄解放ダメージ
        RyugekiDamage(Player.Instance.m_armsMasterTable[4]._atk * 3, true, m_changeValueInterval);
        m_enemyAnim.SetBool("IsDamaged", true);
    }
}