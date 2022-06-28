using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ScenarioManager : MonoBehaviour
{
    static ScenarioManager m_instance;
    public static ScenarioManager Instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = new ScenarioManager();
            }
            return m_instance;
        }
    }

    [SerializeField] bool m_scenarioFlg;

    [SerializeField] Text[] m_texts = new Text[2];

    public bool m_isEnd = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && m_scenarioFlg)
        {
            //テキストを進める。m_isEndがtrueならそのまま終わらせる。
            if(m_isEnd == false)
            {
                CustomPlayableBehaviour.scenarioTextIndexer++;
                CustomPlayableBehaviour.m_director.time = 0;
                CustomPlayableBehaviour.m_director.playableGraph.GetRootPlayable(0).SetSpeed(1d);
            }
            else
            {
                CustomPlayableBehaviour.m_director.playableGraph.GetRootPlayable(0).SetSpeed(1d);
            }
        }
    }

    public void TextEnd()
    {
        foreach(var t in m_texts)
        {
            t.text = "";
        }
    }
}
