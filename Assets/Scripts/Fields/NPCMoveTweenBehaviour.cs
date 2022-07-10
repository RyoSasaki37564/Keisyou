using UnityEngine;
using UnityEngine.Playables;

public class NPCMoveTweenBehaviour : PlayableBehaviour
{
    public Vector3 m_startingPosition;
    public Transform[] m_spots;
    public float m_deltaTime;
    public int[] m_lenges;
    public int m_fullLenge;
    /// <summary> １スタートのカウンター。 </summary>
    public int m_turningCount = 1;
    public bool m_isEnd;
    int m_onBehaviourPlayCount;
    public int m_countOfOnBehaviourPlay { get => m_onBehaviourPlayCount; }

    public override void PrepareFrame(Playable playable, FrameData info)
    {
        if (m_spots[0])
        {
            m_startingPosition = m_spots[0].position;
        }
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        m_onBehaviourPlayCount++;
    }
}
