﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// シーン間で持ち越したいデータの支配クラス
/// </summary>
/// 
[RequireComponent(typeof(Encount))]
[RequireComponent(typeof(Inventory))]
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

    public int m_exp;

    public int m_money;
    
    /// <summary> 技量。スキルツリーの解放に使う。 </summary>
    public int m_tp;


    //アビリティツリー関連


    bool m_ryugekiUnLockFlg;
    public bool GetRyugekiUnLockFlg { get => m_ryugekiUnLockFlg; }
    bool[] m_nineKeyActiveFlgs = new bool[9];
    public bool GetNineKeyActivateFlgs(int id)
    {
        return Instance.m_nineKeyActiveFlgs[id];
    }

    /// <summary>回避の心得 0 = 「足捌き」, 1 = 「体捌き」 </summary>
    bool[] m_dodgeAbilitiesActiveFlgs = new bool[2];
    public bool GetDodgeAbilitiesActivateFlgs(int id)
    {
        return Instance.m_dodgeAbilitiesActiveFlgs[id];
    }

    //アビリティツリー関連


    [System.NonSerialized] public Encount m_encountData;

    public Inventory m_testInventry = new Inventory();

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

            LoadedStatusSetting();


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

            if(m_encountData == null)
            {
                m_encountData = GetComponent<Encount>();
            }

            m_testInventry.TestInventryMake();

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void SaveStatus()
    {
        // ステータスをセーブする。
        SaveDataStructure.m_gameData.playerStatus[0] = m_stuts.m_level;
        SaveDataStructure.m_gameData.playerStatus[1] = m_stuts.m_hp;
        SaveDataStructure.m_gameData.playerStatus[2] = m_stuts.m_con;
        SaveDataStructure.m_gameData.playerStatus[3] = m_stuts.m_dodge;
        SaveDataStructure.m_gameData.playerStatus[4] = m_stuts.m_atk;
        SaveDataStructure.m_gameData.playerStatus[5] = m_stuts.m_def;
        SaveDataStructure.m_gameData.playerStatus[6] = m_nowKakugo;

        SaveDataStructure.m_gameData.playerPoints[0] = m_exp;
        SaveDataStructure.m_gameData.playerPoints[1] = m_money;
        SaveDataStructure.m_gameData.playerPoints[2] = m_tp;

        SaveDataStructure.m_instance.SavePlayerData();
    }

    void LoadedStatusSetting()
    {
        //SaveDataStructure.m_instance.LoadData();

        PlayerStatusAlfa psa = new PlayerStatusAlfa(SaveDataStructure.m_gameData.playerStatus[0], SaveDataStructure.m_gameData.playerStatus[1],
            SaveDataStructure.m_gameData.playerStatus[2], SaveDataStructure.m_gameData.playerStatus[3], SaveDataStructure.m_gameData.playerStatus[4],
            SaveDataStructure.m_gameData.playerStatus[5], SaveDataStructure.m_gameData.playerStatus[6]);
        m_stuts = psa;

        m_exp = SaveDataStructure.m_gameData.playerPoints[0];
        m_money = SaveDataStructure.m_gameData.playerPoints[1];
        m_tp = SaveDataStructure.m_gameData.playerPoints[2];
    }

    //アビリティ解放系

    public void NinekeyActivate(int id)
    {
        if(id == 4)
        {
            // 第五番「皆」は龍撃そのものの解放も行う
            Instance.m_ryugekiUnLockFlg = true;
        }
        Instance.m_nineKeyActiveFlgs[id] = true;
    }

    public void DodgeAbilitiesActivate(int id)
    {
        Instance.m_dodgeAbilitiesActiveFlgs[id] = true;
    }

    //アビリティ解放系
}
