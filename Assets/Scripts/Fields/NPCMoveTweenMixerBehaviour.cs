using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class NPCMoveTweenMixerBehaviour : PlayableBehaviour
{
    bool m_FirstFrameHappened;

    int m_zone;

    Animator m_anim;
    public Animator m_walkingAnim { get => m_anim; }

    int m_nowOnPlay = 1;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        GameObject trackBinding = playerData as GameObject;

        if (trackBinding == null)
            return;

        Vector3 defaultPosition = trackBinding.transform.position;

        int inputCount = playable.GetInputCount();

        for (int i = 0; i < inputCount; i++)
        {
            if(i != m_zone)
            {
                continue;
            }

            ScriptPlayable<NPCMoveTweenBehaviour> playableInput = (ScriptPlayable<NPCMoveTweenBehaviour>)playable.GetInput(i);
            NPCMoveTweenBehaviour input = playableInput.GetBehaviour();

            if (input.m_spots[input.m_turningCount] == null)
                continue;

            if(!m_FirstFrameHappened)
            {
                m_anim = trackBinding.GetComponent<Animator>();
                if(!m_walkingAnim.enabled)
                {
                    m_walkingAnim.enabled = true;
                }
                AnimChange(input, m_walkingAnim);
                if(!input.m_spots[0])
                {
                    input.m_startingPosition = defaultPosition;
                }
                m_FirstFrameHappened = true;
            }

            if(m_nowOnPlay < input.m_countOfOnBehaviourPlay)
            {
                if (!m_walkingAnim.enabled)
                {
                    m_walkingAnim.enabled = true;
                }
                m_walkingAnim.SetBool("SetIdle", false);
                AnimChange(input, m_walkingAnim);
                m_nowOnPlay = input.m_countOfOnBehaviourPlay;
            }

            var progress = (float)((playableInput.GetTime() - input.m_deltaTime) / playableInput.GetDuration() *  input.m_fullLenge / input.m_lenges[input.m_turningCount - 1]);

            var currentX = Mathf.Lerp(input.m_spots[input.m_turningCount - 1].position.x,
                input.m_spots[input.m_turningCount].position.x, progress);

            var currentY = Mathf.Lerp(input.m_spots[input.m_turningCount - 1].position.y,
                input.m_spots[input.m_turningCount].position.y, progress);

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
                if (input.m_turningCount == input.m_spots.Length - 1)
                {
                    if(!input.m_isEnd)
                    {
                        m_zone++;
                        m_nowOnPlay = 0;
                        if (!m_walkingAnim.enabled)
                        {
                            m_walkingAnim.enabled = true;
                        }
                        m_walkingAnim.SetBool("SetIdle", true);
                        input.m_isEnd = true;
                    }
                }
                else
                {
                    if (!m_walkingAnim.enabled)
                    {
                        m_walkingAnim.enabled = true;
                    }
                    m_walkingAnim.SetBool("SetIdle", false);
                    input.m_turningCount++;
                    input.m_deltaTime = (float)playableInput.GetTime();
                    AnimChange(input, m_walkingAnim);
                }
            }
        }
    }

    public static void AnimChange(NPCMoveTweenBehaviour behaviour, Animator anim)
    {
        var moveVector = behaviour.m_spots[behaviour.m_turningCount - 1].position - behaviour.m_spots[behaviour.m_turningCount].position;
        float animValue = Mathf.Abs(moveVector.y) >= Mathf.Abs(moveVector.x) ? moveVector.y : moveVector.x;
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
