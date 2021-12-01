using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Player : BattleChara
{
    public static Player Instance = default;

    public float m_maxConcentlate { get; set; }
    public float m_concentlate { get; set; }

    [SerializeField] TextAsset m_playerLevelTableText = default; //プレイヤーのステータス
    StringReader sr;

    public float m_dogePower { get; set; }

    [SerializeField] Slider m_hpSlider = default;
    [SerializeField] Slider m_conSlider = default;
    [SerializeField] Slider m_dgSlider = default;

    /// <summary> プレイヤーレベルテーブルの最大行数＆レベル上限 開発段階では99 </summary>
    int m_playerLevelTableLineMax;

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
    public PlayerStatus[] m_playerLevelTable = new PlayerStatus[99];

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
            //マスターデータ格納さん太郎
            sr = new StringReader(m_playerLevelTableText.text);
            m_playerLevelTableLineMax = int.Parse(sr.ReadLine());
            string[] line = sr.ReadLine().Split(','); //2行目はパラメータフォーマットなので読み捨てる。
            for(var i = 0; i < m_playerLevelTableLineMax; i++)
            {

            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_maxHP = 100;

        m_hpSlider.maxValue = m_maxHP;
        m_currentHP = m_maxHP;
        m_hpSlider.value = m_currentHP;

        m_conSlider.maxValue = m_maxConcentlate;
        m_conSlider.value = m_concentlate;

        m_dgSlider.maxValue = m_dogePower;
        m_dgSlider.value = m_dogePower;

    }

    // Update is called once per frame
    void Update()
    {
        m_hpSlider.value = m_currentHP;
        m_conSlider.value = m_concentlate;
        m_dgSlider.value = m_dogePower;
    }
}
