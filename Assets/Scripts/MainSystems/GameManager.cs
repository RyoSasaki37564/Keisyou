using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = default;

    /// <summary> 敵ステータスのマスター </summary>
    [SerializeField] TextAsset m_master = default;
    StringReader sr;
    int m_enemyMasterLineMax; //敵マスターデータの行数

    /// <summary>
    /// 敵マスターデータ構造体。,,e_id,, e_name,, e_hp,,  e_attack,, e_deffence,,
    /// 体力攻撃防御はfloat
    /// </summary>
    public struct EnemyMasterData
    {
        public int e_id;
        public string e_name;
        public float e_hp;
        public float e_attack;
        public float e_deffence;

        public EnemyMasterData(int id, string name, float hp, float attack, float deffence)
        {
            this.e_id = id;
            this.e_name = name;
            this.e_hp = hp;
            this.e_attack = attack;
            this.e_deffence = deffence;
        }
    }
    /// <summary> 敵マスターの配列　,,e_id,, e_name,, e_hp,,  e_attack,, e_deffence,, </summary>
    public EnemyMasterData[] m_enemyMaster;

    void Awake()
    {
        if(Instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            //マスター格納処理
            sr = new StringReader(m_master.text);
            m_enemyMasterLineMax = int.Parse(sr.ReadLine());
            string[] line = sr.ReadLine().Split(',');//2行目はパラメータフォーマットなので読み捨てる
            m_enemyMaster = new EnemyMasterData[m_enemyMasterLineMax];
            if(sr != null)
            {
                for (var i = 0; i < m_enemyMasterLineMax; i++)
                {
                    line = sr.ReadLine().Split(',');
                    m_enemyMaster[i] = new EnemyMasterData(int.Parse(line[0]), line[1],
                        float.Parse(line[2]), float.Parse(line[3]), float.Parse(line[4]));
                }
            }
            else
            {
                Debug.LogError("ますたーがないですます");
            }
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
