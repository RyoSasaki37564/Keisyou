using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class NPCMoveTweenClip : PlayableBehaviour
{
    public static PlayableDirector m_director;
    [System.NonSerialized] public GameObject m_templateGameObject;

    [SerializeField] GameObject m_charactor;

    [SerializeField] Vector3 m_startPos;
    [SerializeField] Vector3 m_goalPos;

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
        var progress = (float)(playable.GetTime() / playable.GetDuration());
        var currentX = Mathf.Lerp(m_startPos.x, m_goalPos.x, progress);
        var currentY = Mathf.Lerp(m_startPos.y, m_goalPos.y, progress);

        m_charactor.transform.position = new Vector3(currentX, currentY, m_charactor.transform.position.z);
    }
}
