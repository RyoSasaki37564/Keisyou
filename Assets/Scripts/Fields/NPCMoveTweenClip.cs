using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class NPCMoveTweenClip : PlayableAsset, ITimelineClipAsset
{
    public NPCMoveTweenBehaviour m_template = new NPCMoveTweenBehaviour();
    public ExposedReference<Transform> m_startPoint;
    public ExposedReference<Transform> m_endPoint;

    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending; }
    }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<NPCMoveTweenBehaviour>.Create(graph, m_template);
        NPCMoveTweenBehaviour clone = playable.GetBehaviour();
        clone.m_startLocation = m_startPoint.Resolve(graph.GetResolver());
        clone.m_endLocation = m_endPoint.Resolve(graph.GetResolver());
        return playable;
    }



    /*
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
    */
}
