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
}
