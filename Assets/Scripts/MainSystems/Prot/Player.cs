﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Player : BattleChara
{
    public static Player Instance = default;

    [ContextMenuItem("レベル+１", "LevelUP")] public int m_playerLevel;

    /// <summary> 覚悟値 </summary>
    public int m_preparedness { get; set; }

    /// <summary> 集中力の最大 </summary>
    public int m_maxConcentlate { get; set; }

    /// <summary> 集中力の現在量 </summary>
    public int m_currentConcentlate { get; set; }

    /// <summary> レベルテーブルのCSV </summary>
    [SerializeField] TextAsset m_playerLevelTableText = default;
    StringReader sr;

    /// <summary> 武器マスターテーブルのCSV </summary>
    [SerializeField] TextAsset m_armsMasterTableText = default;
    StringReader armsSR;
    /// <summary>
    /// 武器マスターの最大行数
    /// </summary>
    int m_armsTableLineMax;

    /// <summary> アイテムのマスターテーブルのCSV </summary>
    [SerializeField] TextAsset m_itemMasterTableText = default;
    StringReader itemSR;
    int m_itemTableLineMax;

    /// <summary> 回避率の最大 </summary>
    public float m_dogePowerMax { get; set; }
    /// <summary> 回避率の現在量 </summary>
    public float m_currentDogePower { get; set; }

    [SerializeField] Slider m_hpSlider = default;
    [SerializeField] Slider m_conSlider = default;
    [SerializeField] Slider m_dgSlider = default;

    [SerializeField] Text m_hpText = default; //体力の表記
    [SerializeField] Text m_conText = default; //集中の表記
    [SerializeField] Text m_dogeText = default; //回避率の表記
    //[SerializeField] Text m_prepText = default; //覚悟の表記

    /// <summary> プレイヤーレベルテーブルの最大行数＆レベル上限 開発段階では99 </summary>
    int m_playerLevelTableLineMax;

    [SerializeField] SpriteRenderer m_Chishibuki = default;

    /// <summary>
    /// 0 = ダメージ, 1 = HP回復, 2 = 集中回復, 3 = 死
    /// </summary>
    [SerializeField] SEPlay[] m_SEs = new SEPlay[4]; 

    /// <summary>　レベルテーブルマスターデータの内容　</summary>
    public struct PlayerStatus
    {
        public int p_level;
        public float p_hp;
        public float p_attack;
        public float p_deffence;
        public float p_doge;
        public int p_concentlate;

        public PlayerStatus(int lv, float hp, float atk, float def, float dog, int con)
        {
            this.p_level = lv;
            this.p_hp = hp;
            this.p_attack = atk;
            this.p_deffence = def;
            this.p_doge = dog;
            this.p_concentlate = con;
        }
    }
    /// <summary> レベルテーブルの本体 </summary>
    public PlayerStatus[] m_playerLevelTable;

    /// <summary>　武器データの内容　</summary>
    public struct ToryuguStuts
    {
        public int _id;
        public string _name;
        public int _atk;
        /// <summary>
        /// 0 = 花 ,1 = 鳥, 2 = 風, 3 = 月
        /// 相性は　0 > 1 > 2 > 0。　月(3)は全属性と有利を取り合う。
        /// </summary>
        public int _type;
        public int _ryugeki; //龍撃補正値
        public bool _isGet; //所持の有無

        public ToryuguStuts(int id, string name, int atk, int type, int ryu, bool get)
        {
            this._id = id;
            this._name = name;
            this._atk = atk;
            this._type = type;
            this._ryugeki = ryu;
            this._isGet = get;
        }
    }
    /// <summary> 武器データテーブルの本体 </summary>
    public ToryuguStuts[] m_armsMasterTable;

    /// <summary> アイテムデータ内容 </summary>
    public struct ItemStuts
    {
        public int _id;
        public string _name;
        public string _InfoText;
        public string _frvText;
        public int _weight;
        public int _count;

        public ItemStuts(int id, string name, string info, string frv, int weight, int count)
        {
            this._id = id;
            this._name = name;
            this._InfoText = info;
            this._frvText = frv;
            this._weight = weight;
            this._count = count;
        }
    }
    /// <summary> アイテムマスターの本体 </summary>
    public ItemStuts[] m_itemMasterTable;

    [SerializeField] GameObject m_fadeOut = default;

    bool m_goTitle = false;

    private void Awake()
    {
        m_goTitle = false;
        m_fadeOut.SetActive(false);

        if (Instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            //DontDestroyOnLoad(this.gameObject);

            //レベルマスターデータ格納
            sr = new StringReader(m_playerLevelTableText.text);
            m_playerLevelTableLineMax = int.Parse(sr.ReadLine());
            m_playerLevelTable = new PlayerStatus[m_playerLevelTableLineMax];
            string[] line = sr.ReadLine().Split(','); //2行目はパラメータフォーマットなので読み捨てる。
            //レベルテーブル生成
            if(sr != null)
            {
                for (var i = 0; i < m_playerLevelTableLineMax; i++)
                {
                    line = sr.ReadLine().Split(',');
                    m_playerLevelTable[i] = new PlayerStatus(int.Parse(line[0]), float.Parse(line[1]),
                        float.Parse(line[2]), float.Parse(line[3]), float.Parse(line[4]), int.Parse(line[5]));
                }
            }
            else
            {
                Debug.LogError("reberuますたーがないですます");
            }

            //レベル１のプレイヤーを生成
            m_playerLevel = 1;
            Leveling(m_playerLevel);


            //武器マスター格納
            armsSR = new StringReader(m_armsMasterTableText.text);
            m_armsTableLineMax = int.Parse(armsSR.ReadLine());
            m_armsMasterTable = new ToryuguStuts[m_armsTableLineMax];
            line = armsSR.ReadLine().Split(',');
            //武器データテーブル生成
            if (armsSR != null)
            {
                //初期装備のみ解放しておく
                line = armsSR.ReadLine().Split(',');
                m_armsMasterTable[0] = new ToryuguStuts(int.Parse(line[0]), line[1],
                    int.Parse(line[2]), int.Parse(line[3]), int.Parse(line[4]), true);

                for (var i = 1; i < m_armsTableLineMax; i++)
                {
                    line = armsSR.ReadLine().Split(',');
                    m_armsMasterTable[i] = new ToryuguStuts(int.Parse(line[0]), line[1],
                        int.Parse(line[2]), int.Parse(line[3]), int.Parse(line[4]), false);

                    //開発用に今だけ全開放
                    m_armsMasterTable[i]._isGet = true;

                }
            }
            else
            {
                Debug.LogError("bukiますたーがないですます");
            }

            //アイテムマスター格納
            itemSR = new StringReader(m_itemMasterTableText.text);
            m_itemTableLineMax = int.Parse(itemSR.ReadLine());
            m_itemMasterTable = new ItemStuts[m_itemTableLineMax];
            line = itemSR.ReadLine().Split(',');
            //アイテムマスターデータ作成
            if(itemSR != null)
            {
                for(var i = 0; i < m_itemTableLineMax; i++)
                {
                    line = itemSR.ReadLine().Split(',');
                    m_itemMasterTable[i] = new ItemStuts(int.Parse(line[0]), line[1], line[2], line[3], int.Parse(line[4]), 2);
                }
            }
            else
            {
                Debug.LogError("アイテムマスターがねーぞオイ！");
            }
        }
    }

    public override float Damage(float damage, bool isUnDeffencive)
    {
        m_SEs[0].MyPlayOneShot();

        float x = (damage - (damage * m_deffence)) / this.m_maxHP;

        m_Chishibuki.color = new Color(m_Chishibuki.color.r,
                                        m_Chishibuki.color.g,
                                        m_Chishibuki.color.b, 
                                        m_Chishibuki.color.a + x/2);

        return base.Damage(damage, isUnDeffencive);
    }

    public override void Healing(float medic)
    {
        m_SEs[1].MyPlayOneShot();

        float x = medic / this.m_maxHP;

        m_Chishibuki.color = new Color(m_Chishibuki.color.r,
                                        m_Chishibuki.color.g,
                                        m_Chishibuki.color.b,
                                        m_Chishibuki.color.a - x);
        base.Healing(medic);
    }

    // Start is called before the first frame update
    void Start()
    {
        UISettings();
    }

    // Update is called once per frame
    void Update()
    {
        //ステータスUI描画
        if (m_currentHP <= 0)
        {
            m_hpSlider.value = m_currentHP;
            m_hpText.text = "<color=#8b0000>死</color>";
            if (m_goTitle == false)
            {
                m_SEs[3].MyPlayOneShot();
                m_fadeOut.SetActive(true);
                m_goTitle = true;
            }
        }
        else if (m_currentHP <= m_maxHP)
        {
            m_hpSlider.value = m_currentHP;
            m_hpText.text = m_currentHP.ToString();
        }
        else if(m_currentHP > m_maxHP)
        {
            m_currentHP = m_maxHP;
        }

        if(m_currentConcentlate <= m_maxConcentlate)
        {
            m_conSlider.value = m_currentConcentlate;
            m_conText.text = m_currentConcentlate.ToString();
        }
        else
        {
            m_currentConcentlate = m_maxConcentlate;
        }

        if(m_currentDogePower <= m_dogePowerMax)
        {
            m_dgSlider.value = m_currentDogePower;
            m_dogeText.text = m_currentDogePower.ToString();
        }
        else
        {
            m_currentDogePower = m_dogePowerMax;
        }
    }

    

    /// <summary>
    /// レベル指定式。
    /// </summary>
    /// <param name="lv">指定するレベル</param>
    public void Leveling(int lv)
    {
        if(lv < 100 && lv > 0)
        {
            int x = lv - 1;
            m_playerLevel = lv;
            this.m_maxHP = m_playerLevelTable[x].p_hp;
            this.m_attack = m_playerLevelTable[x].p_attack;
            this.m_deffence = m_playerLevelTable[x].p_deffence;
            this.m_dogePowerMax = m_playerLevelTable[x].p_doge;
            this.m_maxConcentlate = m_playerLevelTable[x].p_concentlate;
        }
        else
        {
            Debug.LogWarning("無効なレベルを入れるなよん");
        }
    }
    /// <summary> レベルを一つ上げる。 </summary>
    public void LevelUP()
    {
        if(this.m_playerLevel != 99)
        {
            this.m_playerLevel++;
            this.m_maxHP = m_playerLevelTable[m_playerLevel - 1].p_hp;
            this.m_attack = m_playerLevelTable[m_playerLevel - 1].p_attack;
            this.m_deffence = m_playerLevelTable[m_playerLevel - 1].p_deffence;
            this.m_dogePowerMax = m_playerLevelTable[m_playerLevel - 1].p_doge;
            this.m_maxConcentlate = m_playerLevelTable[m_playerLevel - 1].p_concentlate;

            UISettings();
        }
        else
        {
            Debug.LogWarning("これ以上レベルは上がりましぇ～ん");
        }
    }

    /// <summary>
    /// ステータスUI初期化処理
    /// </summary>
    void UISettings()
    {
        m_hpSlider.maxValue = m_maxHP;
        m_currentHP = m_maxHP;
        m_hpSlider.value = m_currentHP;

        m_conSlider.maxValue = m_maxConcentlate;
        m_conSlider.value = m_currentConcentlate;

        m_dgSlider.maxValue = m_dogePowerMax;
        m_currentDogePower = m_dogePowerMax;
        m_dgSlider.value = m_currentDogePower;
    }


    /// <summary>
    /// 集中回復処理
    /// </summary>
    /// <param name="medic"></param>
    public void Relax(int rel)
    {
        m_SEs[2].MyPlayOneShot();
        m_currentConcentlate += rel;
        if (m_currentConcentlate > m_maxConcentlate)
        {
            m_currentConcentlate = m_maxConcentlate;
        }
    }

}
