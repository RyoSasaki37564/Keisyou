using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public struct GameDataStruct
{
    /// <summary> 0 = level, 1 = hp, 2 = con, 3 = dod, 4 = atk, 5 = def, 6 = kak, 7 = exp, 8 = money, 9 = tp </summary>
    public int[] playerStatusAndPoints;

    //public int[] items;

    //public bool[] abilityActivateFlgs;

    //public bool[] senarioFlgs;

    //public int[] eventFlgs;
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

        m_instance = new SaveDataStructure();
        m_gameData.playerStatusAndPoints = new int[10];

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
        Debug.Log(m_gameData + "のデータをロード");
        string datastr = "";
        StreamReader reader;

        reader = new StreamReader(Application.dataPath + "/save" + m_saveName + ".json");
        datastr = reader.ReadToEnd();
        reader.Close();

        m_gameData = JsonUtility.FromJson<GameDataStruct>(datastr); // ロードしたデータで上書き
        Debug.Log(m_gameData + "のデータをロードしました");
    }

    void SaveDataCheak()
    {
        string datastr = "";
        StreamReader reader;

        reader = new StreamReader(Application.dataPath + "/save" + m_saveName + ".json");
        datastr = reader.ReadToEnd();
        reader.Close();

        m_oneTimeData = JsonUtility.FromJson<GameDataStruct>(datastr);

        for (int i = 0; i < m_gameData.playerStatusAndPoints.Length; i++)
        {
            if (m_oneTimeData.playerStatusAndPoints[i] > m_gameData.playerStatusAndPoints[i])
            {
                m_gameData.playerStatusAndPoints[i] = m_oneTimeData.playerStatusAndPoints[i];
            }
        }
    }
}
