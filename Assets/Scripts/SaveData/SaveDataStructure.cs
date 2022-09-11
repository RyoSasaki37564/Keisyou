using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public struct GameDataStruct
{
    /// <summary> 0 = level, 1 = hp, 2 = con, 3 = dod, 4 = atk, 5 = def, 6 = kak </summary>
    public int[] playerStatus;

    /// <summary> 0 = exp, 1 = money, 2 = tp </summary>
    public int[] playerPoints;

    //public int[] armsInventry;

    //public int[] mainArms;

    //public int[] itemsInventry;

    //public int[] shortCutItems;

    //public bool[][] abilityActivateFlgs;

    //public bool[] senarioFlgs;

    //public int[] eventFlgs;

    //public bool[] tutrials;
}

public class SaveDataStructure : MonoBehaviour
{
    public static SaveDataStructure m_instance;

    public static GameDataStruct m_gameData;
    static GameDataStruct m_oneTimeData;

    [Header("セーブデータの名前"), SerializeField] string m_saveName = "playData";

    private void Awake()
    {
        if (m_instance != null)
        {
            Destroy(this);
            return;
        }

        m_instance = this;
        m_gameData.playerStatus = new int[7];
        m_gameData.playerPoints = new int[3];

        LoadData();
    }

    public void SavePlayerData()
    {
        SaveDataCheak();
        Debug.Log(m_gameData + "のデータをセーブしました");
        StreamWriter writer;

        string jsonstr = JsonUtility.ToJson(m_gameData);

        writer = new StreamWriter(Application.dataPath + "/save" + m_saveName + ".json", false);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }

    public void LoadData()
    {
        //Debug.Log(m_gameData + "のデータをロード");
        string datastr = "";
        StreamReader reader;

        reader = new StreamReader(Application.dataPath + "/save" + m_saveName + ".json");
        datastr = reader.ReadToEnd();
        reader.Close();

        m_gameData = JsonUtility.FromJson<GameDataStruct>(datastr); // ロードしたデータで上書き
        //Debug.Log(m_gameData + "のデータをロードしました");
    }

    void SaveDataCheak()
    {
        string datastr = "";
        StreamReader reader;

        reader = new StreamReader(Application.dataPath + "/save" + m_saveName + ".json");
        datastr = reader.ReadToEnd();
        reader.Close();

        m_oneTimeData = JsonUtility.FromJson<GameDataStruct>(datastr);

        for (var i = 0; i < m_gameData.playerStatus.Length; i++)
        {
            if (m_oneTimeData.playerStatus[i] > m_gameData.playerStatus[i])
            {
                m_gameData.playerStatus[i] = m_oneTimeData.playerStatus[i];
            }
        }
        for(var i = 0; i < m_gameData.playerPoints.Length; i++)
        {
            if(m_oneTimeData.playerPoints[i] > m_gameData.playerPoints[i])
            {
                m_gameData.playerPoints[i] = m_oneTimeData.playerPoints[i];
            }
        }
    }
}
