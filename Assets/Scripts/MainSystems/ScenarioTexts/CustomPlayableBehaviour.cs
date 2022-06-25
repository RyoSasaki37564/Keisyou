using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

[System.Serializable]
public class CustomPlayableBehaviour : PlayableBehaviour
{
    public static PlayableDirector m_director;
    [System.NonSerialized] public GameObject m_templateGameObject;

    Text m_scenarioText;
    Text m_speakerNameText;
    [SerializeField] string m_name = "しゃべってるやつの名前";
    [SerializeField, TextArea(1, 3)] string m_scenario = "龍滅の刃...?\n" + "龍滅の刃...!?";

    [SerializeField] double m_changeSpeed = 1d;

    public bool m_pauseScheduled = false;

    [SerializeField] float m_waitSeconds = 2f;

    public bool m_susumu = false;

    bool m_isAccel = false;

    public override void OnPlayableCreate(Playable playable)
    {
        if(m_director == null)
        {
            m_director = playable.GetGraph().GetResolver() as PlayableDirector;
        }

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
        m_susumu = true;
    }

    //	PlayableTrack停止時
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if(m_pauseScheduled)
        {
            m_director.playableGraph.GetRootPlayable(0).SetSpeed(0d);
            m_pauseScheduled = false;
        }
    }

    /// <summary>
    /// PlayableTrack再生時毎フレーム
    /// つまりUpdate()みてーなもんだな～？多分、メイビー、恐らく、きっと。
    /// </summary>
    public override void PrepareFrame(Playable playable, FrameData info)
    {
        if (m_scenario != null)
        {
            var progress = (float)(playable.GetTime() / playable.GetDuration());
            var current = Mathf.Lerp(0, m_scenario.Length, progress);
            var count = Mathf.CeilToInt(current);

            string s = m_scenario.Substring(0, count);
            m_scenarioText.text = s;

            //シナリオ末に到達
            if(s == m_scenario && m_susumu == true)
            {
                m_pauseScheduled = true;
                OnBehaviourPause(playable, info);
                m_susumu = false;
            }
        }
    }
}