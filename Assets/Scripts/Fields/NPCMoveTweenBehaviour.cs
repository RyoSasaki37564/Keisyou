using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class NPCMoveTweenBehaviour : PlayableBehaviour
{
    public Transform m_startLocation;
    public Transform m_endLocation;
    public Vector3 m_startingPosition;
    public override void PrepareFrame(Playable playable, FrameData info)
    {
        if (m_startLocation)
        {
            m_startingPosition = m_startLocation.position;
        }
    }
}
