﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : BattleChara
{
    /// <summary> 戦闘画面における敵のビジュアル管理オブジェクト </summary>
    [SerializeField] GameObject[] m_enemyGrahicsBody = default;

    /// <summary> 固有特異行動 </summary>
    [SerializeField] GameObject[] m_enemySpecialMove = default;

    /// <summary> エンカウントした敵のIDを格納するリスト </summary>
    public static List<int> m_encountEnemyID = new List<int>();

    public static List<Enemy> m_enemies; //戦闘ごとに設定された数の敵実体を格納

    string m_enemyName = default; //おなまえ

    /// <summary> Canvas下にある敵UI格納用親オブジェクト </summary>
    [SerializeField] GameObject m_enemyObjectsInCanvas = default;

    [SerializeField] GameObject[] m_enemyUI = default; //敵体力バーなどの元

    public int m_statmina { get; set; }

    /// <summary> なまえ　たいりょく　こうげき　ぼうぎょ すたみな</summary>
    public Enemy(string name, float hp, float atk, float def, int stamina)
    {
        this.m_enemyName = name;
        this.m_maxHP = hp;
        this.m_attack = atk;
        this.m_deffence = def;
        this.m_statmina = stamina;
    }

    // Start is called before the first frame update
    void Start()
    {
        //MonoBehaviourを継承したクラスではListの初期化にコンストラクタが使えないらしい。ので、ここで初期化命令を行う。
        m_enemies = new List<Enemy>();
        
        //テスト用エンカウント
        m_encountEnemyID.Add(0);
        
        EnemyGenerate();
    }

    // Update is called once per frame
    void Update()
    {
        //敵インスタンスはstaticなので、死亡したタイミングで消去
        if(this.m_isDead)
        {
            m_enemies.Remove(this);
        }
    }

    void EnemyGenerate()
    {
        if(GameManager.Instance)
        {
            //エンカウントした敵の数だけ敵の実体を作る
            for (var i = 0; i < m_encountEnemyID.Count; i++)
            {
                Debug.Log(m_encountEnemyID[i]);
                Enemy ene = new Enemy(GameManager.Instance.m_enemyMaster[m_encountEnemyID[i]].e_name,
                    GameManager.Instance.m_enemyMaster[m_encountEnemyID[i]].e_hp,
                    GameManager.Instance.m_enemyMaster[m_encountEnemyID[i]].e_attack,
                    GameManager.Instance.m_enemyMaster[m_encountEnemyID[i]].e_deffence,
                    GameManager.Instance.m_enemyMaster[m_encountEnemyID[i]].e_stamina);

                m_enemies.Add(ene);

                //UIを生成してパラメータを反映
                var x = Instantiate(m_enemyUI[i], m_enemyUI[0].transform.position,
                    m_enemyObjectsInCanvas.transform.rotation);
                x.transform.SetParent(m_enemyObjectsInCanvas.transform);
                x.transform.localScale = new Vector3(1, 1, 1); //これ入れないとHPバーが世界を埋め尽くすほどバカデカくなるにょ。
                Slider enemyHPSL = x.GetComponentInChildren<Slider>();
                enemyHPSL.maxValue = m_enemies[i].m_maxHP;
                enemyHPSL.value = m_enemies[i].m_maxHP;
                Text enemyNameText = x.GetComponentInChildren<Text>();
                enemyNameText.text = m_enemies[i].m_enemyName;
            }
            m_enemyUI[0].SetActive(false);
        }
        else
        {
            Debug.LogError("ゲームマネージャー行方不明");
        }
    }
}