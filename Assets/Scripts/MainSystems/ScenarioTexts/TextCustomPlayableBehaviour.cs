using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

[System.Serializable]
public class TextCustomPlayableBehaviour : PlayableBehaviour
{
    public static PlayableDirector m_director;
    [System.NonSerialized] public GameObject m_templateGameObject;

    Text m_scenarioText;
    Text m_speakerNameText;
    public static int scenarioTextIndexer = 0;

    public bool m_pauseScheduled = false;

    int m_endPoint;

    bool m_headFlg;

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

    }

    //	タイムライン停止時
    public override void OnGraphStop(Playable playable)
    {
        m_headFlg = false;
    }

    //このPlayableTrack再生時
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        if(!m_headFlg)
        {
            m_speakerNameText = GameObject.Find("SpeakerName").GetComponent<Text>();
            m_scenarioText = GameObject.Find("ScenarioText").GetComponent<Text>();

            for (var i = 0; i < ScenarioManager.Instance.m_scenariosArray.Length; i++)
            {
                if (ScenarioManager.Instance.m_scenariosArray[i] == "end")
                {
                    m_endPoint = i;
                }
            }
            Debug.LogWarning(scenarioTextIndexer + "/" + ScenarioManager.Instance.m_scenariosArray.Length);
            m_director.playableGraph.GetRootPlayable(0).SetSpeed(ScenarioManager.Instance.SpeedByLength(ScenarioManager.Instance.m_scenariosArray[scenarioTextIndexer]));

            ScenarioManager.Instance.m_timeHead = m_director.time;
            m_headFlg = true;
        }
        m_speakerNameText.text = ScenarioManager.Instance.m_name;
    }

    //	PlayableTrack停止時
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if(m_pauseScheduled)
        {
            ScenarioManager.Instance.m_canNext = true;
            m_director.playableGraph.GetRootPlayable(0).SetSpeed(0d);
            m_pauseScheduled = false;
        }
    }

    /// <summary>
    /// PlayableTrack再生時毎フレーム
    /// つまりUpdate()みてーなもんだ...多分、メイビー、恐らく、きっと。
    /// </summary>
    public override void PrepareFrame(Playable playable, FrameData info)
    {
        if (ScenarioManager.Instance.m_scenariosArray[scenarioTextIndexer] != null && 
            m_director.playableGraph.GetRootPlayable(0).GetSpeed() > 0)
        {
            var progress = (float)(playable.GetTime() / playable.GetDuration());
            var current = Mathf.Lerp(0, ScenarioManager.Instance.m_scenariosArray[scenarioTextIndexer].Length, progress);
            var count = Mathf.CeilToInt(current);

            string s = ScenarioManager.Instance.m_scenariosArray[scenarioTextIndexer].Substring(0, count);
            m_scenarioText.text = s;

            //シナリオ末に到達
            if(s == ScenarioManager.Instance.m_scenariosArray[scenarioTextIndexer] && ScenarioManager.Instance.m_isEnd == false)
            {
                if (scenarioTextIndexer == m_endPoint - 1)
                {
                    ScenarioManager.Instance.m_isEnd = true;
                }
                m_pauseScheduled = true;
                OnBehaviourPause(playable, info);
            }
        }
    }
}