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
                Debug.LogWarning("インスタンスがなーい");
                m_instance = new ScenarioManager();
            }
            return m_instance;
        }
    }

    [System.NonSerialized] public bool m_scenarioFlg;

    public string[] m_scenariosArray; //会話内容
    public string m_name = "しゃべってるやつの名前";
    [SerializeField] Text[] m_texts = new Text[2];

    public bool m_isEnd = false;

    public bool m_canNext = false;

    [SerializeField] Animator m_anim;

    [SerializeField] PlayableDirector m_director;

    [SerializeField] int m_speedBaseLength = 30;

    private void Awake()
    {
        if(!m_instance)
        {
            m_instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && m_scenarioFlg && m_canNext == true)
        {
            m_canNext = false;
            //テキストを進める。m_isEndがtrueならそのまま終わらせる。
            if (m_isEnd == true)
            {
                CustomPlayableBehaviour.m_director.playableGraph.GetRootPlayable(0).SetSpeed(5d);
            }
            else
            {
                CustomPlayableBehaviour.scenarioTextIndexer++;
                CustomPlayableBehaviour.m_director.time = 0;
                CustomPlayableBehaviour.m_director.playableGraph.GetRootPlayable(0).SetSpeed(SpeedByLength(m_scenariosArray[CustomPlayableBehaviour.scenarioTextIndexer]));
            }
        }
    }

    public void ScenarioModeON()
    {
        m_anim.SetBool("AnimFlg", true);
        m_scenarioFlg = true;
    }

    //タイムライントラック末尾にシグナルを置き、これをセット。
    public void ScenarioModeOFF()
    {
        m_scenarioFlg = false;
        m_anim.SetBool("AnimFlg", false);
    }

    public void TextStart()
    {
        m_director.Play();
    }

    public void TextEnd()
    {
        foreach(var t in m_texts)
        {
            t.text = "";
        }
        CustomPlayableBehaviour.scenarioTextIndexer = 0;
        m_isEnd = false;
    }
    public double SpeedByLength(string s)
    {
        double textSpeed = 1d;
        if (s.Length < m_speedBaseLength)
        {
            textSpeed = (float)(m_speedBaseLength / s.Length);
        }
        return textSpeed;
    }
}
