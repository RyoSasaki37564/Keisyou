﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Player : BattleChara
{
    public static Player Instance = default;

    public int m_playerLevel { get; set; }

    /// <summary> 集中力の最大 </summary>
    public float m_maxConcentlate { get; set; }

    /// <summary> 集中力の現在量 </summary>
    public float m_currentConcentlate { get; set; }

    /// <summary> レベルテーブルのCSV </summary>
    [SerializeField] TextAsset m_playerLevelTableText = default;
    StringReader sr;

    /// <summary> 回避率の最大 </summary>
    public float m_dogePowerMax { get; set; }
    /// <summary> 回避率の現在量 </summary>
    public float m_currentDogePower { get; set; }

    [SerializeField] Slider m_hpSlider = default;
    [SerializeField] Slider m_conSlider = default;
    [SerializeField] Slider m_dgSlider = default;

    [SerializeField] Text m_dogeText = default; //回避率の表記

    /// <summary> プレイヤーレベルテーブルの最大行数＆レベル上限 開発段階では99 </summary>
    int m_playerLevelTableLineMax;

    /// <summary>　レベルテーブルマスターデータの内容　</summary>
    public struct PlayerStatus
    {
        public int p_level;
        public float p_hp;
        public float p_attack;
        public float p_deffence;
        public float p_doge;
        public float p_concentlate;

        public PlayerStatus(int lv, float hp, float atk, float def, float dog, float con)
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

    private void Awake()
    {
        if(Instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            //マスターデータ格納
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
                        float.Parse(line[2]), float.Parse(line[3]), float.Parse(line[4]), float.Parse(line[5]));
                }
            }
            else
            {
                Debug.LogError("ますたーがないですます");
            }

            //レベル１のプレイヤーを生成
            m_playerLevel = 1;
            Leveling(m_playerLevel);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_hpSlider.maxValue = m_maxHP;
        m_currentHP = m_maxHP;
        m_hpSlider.value = m_currentHP;

        m_conSlider.maxValue = m_maxConcentlate;
        m_conSlider.value = m_currentConcentlate;

        m_dgSlider.maxValue = m_dogePowerMax;
        m_dgSlider.value = m_dogePowerMax;

    }

    // Update is called once per frame
    void Update()
    {
        m_hpSlider.value = m_currentHP;
        m_conSlider.value = m_currentConcentlate;
        m_dgSlider.value = m_currentDogePower;
        m_dogeText.text = m_currentDogePower.ToString();
    }

    /// <summary>
    /// レベル指定式。
    /// </summary>
    /// <param name="lv">指定するレベル</param>
    public void Leveling(int lv)
    {
        int x = lv - 1;
        m_playerLevel = lv;
        this.m_maxHP = m_playerLevelTable[x].p_hp;
        this.m_attack = m_playerLevelTable[x].p_attack;
        this.m_deffence = m_playerLevelTable[x].p_deffence;
        this.m_dogePowerMax = m_playerLevelTable[x].p_doge;
        this.m_maxConcentlate = m_playerLevelTable[x].p_concentlate;
    }
    /// <summary>
    /// レベルを一つ上げる。
    /// </summary>
    public void Leveling()
    {
        this.m_playerLevel++;
        this.m_maxHP = m_playerLevelTable[m_playerLevel--].p_hp;
        this.m_attack = m_playerLevelTable[m_playerLevel--].p_attack;
        this.m_deffence = m_playerLevelTable[m_playerLevel--].p_deffence;
        this.m_dogePowerMax = m_playerLevelTable[m_playerLevel--].p_doge;
        this.m_maxConcentlate = m_playerLevelTable[m_playerLevel--].p_concentlate;
    }
}
