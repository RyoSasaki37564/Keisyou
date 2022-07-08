using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class NPCMoveTweenMixerBehaviour : PlayableBehaviour
{
    bool m_FirstFrameHappened;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        GameObject trackBinding = playerData as GameObject;

        if (trackBinding == null)
            return;

        Vector3 defaultPosition = trackBinding.transform.position;

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

            var progress = (float)(playableInput.GetTime() / playableInput.GetDuration());

            var currentX = Mathf.Lerp(input.m_startLocation.position.x, input.m_endLocation.position.x, progress);
            var currentY = Mathf.Lerp(input.m_startLocation.position.y, input.m_endLocation.position.y, progress);
            trackBinding.transform.position = new Vector3(currentX, currentY, trackBinding.transform.position.z);
        }


        m_FirstFrameHappened = true;
    }
}
