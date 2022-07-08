using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class NPCMoveTweenMixerBehaviour : PlayableBehaviour
{
    float m_progress = 0;
    bool m_FirstFrameHappened;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        Transform trackBinding = playerData as Transform;

        if (trackBinding == null)
            return;

        Vector3 defaultPosition = trackBinding.position;

        int inputCount = playable.GetInputCount();

        for (int i = 0; i < inputCount; i++)
        {
            ScriptPlayable<NPCMoveTweenBehaviour> playableInput = (ScriptPlayable<NPCMoveTweenBehaviour>)playable.GetInput(i);
            NPCMoveTweenBehaviour input = playableInput.GetBehaviour();

            if (input.m_endLocation == null)
                continue;

            if (!m_FirstFrameHappened && !input.m_startLocation)
            {
                input.m_startingPosition = defaultPosition;
            }

            //var progress = (float)(playable.GetTime() / playable.GetDuration());

            m_progress = (float)(playableInput.GetTime() / playableInput.GetDuration());
            Debug.Log(m_progress + "=" + (float)playableInput.GetTime() + "/" + (float)playableInput.GetDuration());

            var currentX = Mathf.Lerp(input.m_startLocation.position.x, input.m_endLocation.position.x, m_progress);
            var currentY = Mathf.Lerp(input.m_startLocation.position.y, input.m_endLocation.position.y, m_progress);
            trackBinding.position = new Vector3(currentX, currentY, trackBinding.position.z);
        }

        //blendedPosition += defaultPosition * (1f - positionTotalWeight);

        //trackBinding.position = blendedPosition;

        m_FirstFrameHappened = true;
    }

    public override void PrepareFrame(Playable playable, FrameData info)
    {
        //m_progress = (float)(playable.GetTime() / playable.GetDuration());
    }
}
