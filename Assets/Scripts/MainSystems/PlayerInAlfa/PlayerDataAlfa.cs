using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


/// <summary>
/// シーン間で持ち越したいデータの支配クラス
/// </summary>
public class PlayerDataAlfa : MonoBehaviour
{
    static PlayerDataAlfa m_instance;
    public static PlayerDataAlfa Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = (PlayerDataAlfa)FindObjectOfType(typeof(PlayerDataAlfa));
                if (m_instance == null)
                {
                    Debug.LogError("インスタンス消失");
                }
            }
            return m_instance;
        }
    }

    /// <summary> エネミーマスターデータテーブルのCSV
    /// 今はデータ持越しするのこれくらいなのでひとまずここに置く
    /// もしかしたらわけるかも
    /// </summary>
    [SerializeField] TextAsset m_enemyMasterAlfaText = default;
    public EnemyStatusAlfa[] m_enemyTable;

    /// <summary> プレイヤーレベルテーブルのCSV </summary>
    [SerializeField] TextAsset m_playerLevelTableText = default;
    StringReader sr;

    PlayerStatusAlfa[] m_playerLevelTable;
    PlayerStatusAlfa m_stuts;
    public PlayerStatusAlfa GetStuts { get => m_stuts; }

    int m_nowKakugo;
    public int GetKakugo { get => m_nowKakugo; }

    private void Awake()
    {
        if(m_instance == null)
        {
            m_instance = this;


            //レベルマスターデータ格納
            sr = new StringReader(m_playerLevelTableText.text);
            int playerLevelTableLineMax = int.Parse(sr.ReadLine());
            m_playerLevelTable = new PlayerStatusAlfa[playerLevelTableLineMax];
            string[] line = sr.ReadLine().Split(','); //2行目はパラメータフォーマットなので読み捨てる。
            //レベルテーブル生成
            if (sr != null)
            {
                for (var i = 0; i < playerLevelTableLineMax; i++)
                {
                    line = sr.ReadLine().Split(',');
                    m_playerLevelTable[i] = new PlayerStatusAlfa ((int)float.Parse(line[0]), (int)float.Parse(line[1]),
                        (int)float.Parse(line[2]), (int)float.Parse(line[3]), (int)float.Parse(line[4]), (int)float.Parse(line[5]), (int)float.Parse(line[6]));
                }
            }

            m_stuts = m_playerLevelTable[9]; //実際には外部保存したステータスを入れる

            m_nowKakugo = m_stuts.m_kakugo;


            sr = new StringReader(m_enemyMasterAlfaText.text);
            int eneLineMax = int.Parse(sr.ReadLine());
            m_enemyTable = new EnemyStatusAlfa[eneLineMax];
            line = sr.ReadLine().Split(','); //2行目はパラメータフォーマットなので読み捨てる。
            if(sr != null)
            {
                for(var i = 0; i < eneLineMax; i++)
                {
                    line = sr.ReadLine().Split(',');
                    m_enemyTable[i] = new EnemyStatusAlfa(int.Parse(line[0]), line[1], int.Parse(line[2]), int.Parse(line[3]),
                        int.Parse(line[4]), int.Parse(line[5]), int.Parse(line[6]));
                }
            }

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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
