using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class NPCMoveTweenMixerBehaviour : PlayableBehaviour
{
    bool m_FirstFrameHappened;

    Animator m_walkingAnim;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        GameObject trackBinding = playerData as GameObject;

        if (trackBinding == null)
            return;

        Vector3 defaultPosition = trackBinding.transform.position;

        int inputCount = playable.GetInputCount();

        for (int i = 0; i < inputCount; i++)
        {
            if(i < NPCMoveTweenBehaviour.m_zone)
            {
                continue;
            }
            if(i > NPCMoveTweenBehaviour.m_zone)
            {
                continue;
            }

            ScriptPlayable<NPCMoveTweenBehaviour> playableInput = (ScriptPlayable<NPCMoveTweenBehaviour>)playable.GetInput(i);
            NPCMoveTweenBehaviour input = playableInput.GetBehaviour();

            if (input.m_spots[input.m_turningCount] == null)
                continue;

            if(!m_FirstFrameHappened)
            {
                m_walkingAnim = trackBinding.GetComponent<Animator>();
                AnimChange(input, m_walkingAnim);
                if(!input.m_spots[0])
                {
                    input.m_startingPosition = defaultPosition;
                }
            }

            var progress = (float)((playableInput.GetTime() - input.m_deltaTime) / playableInput.GetDuration() *  input.m_fullLenge / input.m_lenges[input.m_turningCount - 1]);

            var currentX = Mathf.Lerp(input.m_spots[input.m_turningCount - 1].position.x,
                input.m_spots[input.m_turningCount].position.x,
                progress);

            var currentY = Mathf.Lerp(input.m_spots[input.m_turningCount - 1].position.y,
                input.m_spots[input.m_turningCount].position.y,
                progress);

            if(Mathf.Abs(Mathf.Abs(currentX) - Mathf.Abs(input.m_spots[input.m_turningCount].position.x)) < 0.1f)
            {
                currentX = input.m_spots[input.m_turningCount].position.x;
            }
            if (Mathf.Abs(Mathf.Abs(currentY) - Mathf.Abs(input.m_spots[input.m_turningCount].position.y)) < 0.1f)
            {
                currentY = input.m_spots[input.m_turningCount].position.y;
            }

            trackBinding.transform.position = new Vector3(currentX, currentY, trackBinding.transform.position.z);

            if(trackBinding.transform.position == input.m_spots[input.m_turningCount].position)
            {
                Debug.LogWarning(input.m_turningCount + "/" + (input.m_spots.Length - 1));
                if (input.m_turningCount == input.m_spots.Length - 1)
                {
                    if(!input.m_isEnd)
                    {
                        NPCMoveTweenBehaviour.m_zone++;
                        m_walkingAnim.SetBool("SetIdle", true);
                        input.m_isEnd = true;
                    }
                }
                else
                {
                    m_walkingAnim.SetBool("SetIdle", false);
                    input.m_turningCount++;
                    input.m_deltaTime = (float)playableInput.GetTime();
                    AnimChange(input, m_walkingAnim);
                }
            }
        }
        m_FirstFrameHappened = true;
    }

    void AnimChange(NPCMoveTweenBehaviour behaviour, Animator anim)
    {
        var moveVector = behaviour.m_spots[behaviour.m_turningCount - 1].position - behaviour.m_spots[behaviour.m_turningCount].position;
        float animValue = Mathf.Abs(moveVector.x) <= Mathf.Abs(moveVector.y) ? moveVector.y : moveVector.x;
        if (animValue == moveVector.x)
        {
            anim.CrossFade(animValue < 0 ? "WalkRightAnimation" : "WalkLeftAnimation", 0, 0, 0);
        }
        else
        {
            anim.CrossFade(animValue < 0 ? "WalkFrontAnimation" : "WalkBackAnimation", 0, 0, 0);
        }
    }
}
