using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class NPCMoveTweenClip : PlayableBehaviour
{
    public static PlayableDirector m_director;
    [System.NonSerialized] public GameObject m_templateGameObject;

    [SerializeField] GameObject m_charactor;

    [SerializeField] Transform m_startPos;
    
    [SerializeField] Transform m_goalPos;

    public override void OnPlayableCreate(Playable playable)
    {
        if (m_director == null)
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

    }

    //このPlayableTrack再生時
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {

    }

    //	PlayableTrack停止時
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {

    }

    /// <summary>
    /// PlayableTrack再生時毎フレーム
    /// </summary>
    public override void PrepareFrame(Playable playable, FrameData info)
    {

    }
}
