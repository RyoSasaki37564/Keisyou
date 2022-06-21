using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

[System.Serializable]
public class CustomPlayableBehaviour : PlayableBehaviour
{
    private PlayableDirector director;
    public GameObject templateGameObject;

    [SerializeField] float m_updateSeconds = 0.2f; //表示する文字を更新するスパンの秒数
    float m_time;

    [SerializeField] string m_name = "しゃべってるやつの名前";
    [SerializeField, TextArea(1, 3)] string m_scenario = "龍滅の刃...?\n" + "龍滅の刃...!?";
    Text m_speakerNameText;
    Text m_scenarioText;
    int m_openedCount = 1; //表示文字数

    public override void OnPlayableCreate(Playable playable)
    {
        director = playable.GetGraph().GetResolver() as PlayableDirector;
    }

    //タイムライン開始時
    public override void OnGraphStart(Playable playable)
    {
        m_speakerNameText = GameObject.Find("SpeakerName").GetComponent<Text>();
        m_scenarioText = GameObject.Find("ScenarioText").GetComponent<Text>();
    }

    //	タイムライン停止時
    public override void OnGraphStop(Playable playable)
    {

    }

    //このPlayableTrack再生時
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        m_speakerNameText.text = m_name;
    }

    //	PlayableTrack停止時
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {

    }

    /// <summary>
    /// PlayableTrack再生時毎フレーム
    /// つまりUpdate()みてーなもんだな～？多分、メイビー、恐らく、きっと。
    /// </summary>
    public override void PrepareFrame(Playable playable, FrameData info)
    {
        if(m_scenario != null)
        {
            string s = m_scenario.Substring(0, m_openedCount);

            m_time += Time.deltaTime;

            if (s[s.Length - 1] == '\n')
            {
                //改行に際して2秒遅くする
                if (m_time == m_updateSeconds + 2)
                {
                    m_scenarioText.text = s;
                    if (s != m_scenario)
                    {
                        m_openedCount++;
                    }
                    m_time = 0;
                }
            }
            else
            {
                if (m_time == m_updateSeconds)
                {
                    m_scenarioText.text = s;
                    if (s != m_scenario)
                    {
                        m_openedCount++;
                    }
                    m_time = 0;
                }
            }
        }
    }
}