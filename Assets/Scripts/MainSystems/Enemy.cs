using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Enemy : BattleChara
{
    /// <summary> 戦闘画面における敵のビジュアル管理オブジェクト </summary>
    [SerializeField] GameObject[] m_enemyGrahicsBody = default;

    /// <summary> 固有特異行動 </summary>
    [SerializeField] GameObject[] m_enemySpecialMove = default;

    /// <summary> エンカウントした敵のIDをリスト化 </summary>
    public static List<int> m_encountEnemyID = new List<int>();

    public List<Enemy> m_enemies = new List<Enemy>(); //戦闘ごとに設定された数の敵実体を格納

    private void Awake()
    {
        //テスト用エンカウント
        m_encountEnemyID.Add(0);

        for(var i = 0; i < m_encountEnemyID.Count; i++)
        {

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
