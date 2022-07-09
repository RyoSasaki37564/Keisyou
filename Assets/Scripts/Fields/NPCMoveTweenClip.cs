using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class NPCMoveTweenClip : PlayableAsset, ITimelineClipAsset
{
    public NPCMoveTweenBehaviour m_template = new NPCMoveTweenBehaviour();
    [SerializeField] public List<TurningPoint> m_turningPoints = new List<TurningPoint>();
    int[] m_lenges;
    [System.NonSerialized] public int m_fullLenge;

    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending; }
    }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<NPCMoveTweenBehaviour>.Create(graph, m_template);
        if (m_turningPoints.Count >= 2)
        {
            NPCMoveTweenBehaviour clone = playable.GetBehaviour();
            m_lenges = new int[m_turningPoints.Count - 1];
            clone.m_spots = new Transform[m_turningPoints.Count];
            for (var i = 0; i < m_turningPoints.Count; i++)
            {
                clone.m_spots[i] = m_turningPoints[i].m_spot.Resolve(graph.GetResolver());
                if(i >= 1)
                {
                    m_lenges[i - 1] += (int)(Mathf.Abs(clone.m_spots[i].position.x - clone.m_spots[i - 1].position.x));
                    m_lenges[i - 1] += (int)(Mathf.Abs(clone.m_spots[i].position.y - clone.m_spots[i - 1].position.y));
                    m_fullLenge += m_lenges[i - 1];
                }
            }
            clone.m_lenges = m_lenges;
            clone.m_fullLenge = m_fullLenge;
        }
        else
        {
            Debug.LogError("最低でも始点と終点の二つは作れ～！");
        }
        return playable;
    }
}

[System.Serializable]
public class TurningPoint
{
    [SerializeField] public ExposedReference<Transform> m_spot;
}