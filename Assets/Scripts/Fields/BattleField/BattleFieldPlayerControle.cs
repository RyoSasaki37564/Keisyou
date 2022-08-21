using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BattleFieldPlayerControle : MonoBehaviour
{
    Animator m_anim;
    Rigidbody2D m_rb;

    [SerializeField] float m_speedRate = 2f;
    [SerializeField] float m_dashRate = 2f;
    [SerializeField] float m_dogeSpeed = 5f;

    float m_h = 0, m_v = 0;
    bool m_nowDoge;
    bool m_dogeIntervalFlg;

    [SerializeField] float m_dogeInterval = 2f;

    enum MoveState
    {
        stop,
        walk,
        doge,
        attack,
        dropAttack
    }
    MoveState m_nowState = MoveState.stop;

    enum StandbyDirection
    {
        up,
        down,
        right,
        left,
    }
    StandbyDirection m_nowDirection = StandbyDirection.down;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool dash = false;
        if(m_nowState == MoveState.stop || m_nowState == MoveState.walk)
        {
            m_h = Input.GetAxisRaw("Horizontal");
            m_v = Input.GetAxisRaw("Vertical");
            bool doge = Input.GetKeyDown(KeyCode.LeftControl);
            dash = Input.GetKey(KeyCode.LeftShift);
            if (!m_dogeIntervalFlg && doge && (m_nowState == MoveState.stop || m_nowState == MoveState.walk))
            {
                m_nowState = MoveState.doge;
                m_dogeIntervalFlg = true;
            }
            else if (m_h != 0 || m_v != 0)
            {
                m_nowState = MoveState.walk;
            }
            else if (m_h == 0 && m_v == 0)
            {
                m_nowState = MoveState.stop;
            }
        }
        switch (m_nowState)
        {
            case MoveState.stop:
                m_rb.velocity = Vector2.zero;
                break;
            case MoveState.walk:
                Vector2 move = new Vector2(m_h, m_v);
                move.Normalize();
                float speed = m_speedRate * (dash ? m_dashRate : 1);
                m_rb.velocity = move * speed;

                if (m_h > 0)
                {
                    m_nowDirection = StandbyDirection.right;
                    m_anim.CrossFade("FieldAvaterWalkRightAnimation", 0);
                }
                else if (m_h < 0)
                {
                    m_nowDirection = StandbyDirection.left;
                    m_anim.CrossFade("FieldAvaterWalkLeftAnimation", 0);
                }
                else if (m_v > 0)
                {
                    m_nowDirection = StandbyDirection.up;
                    m_anim.CrossFade("FieldAvaterWalkFrontAnimation", 0);
                }
                else if (m_v < 0)
                {
                    m_nowDirection = StandbyDirection.down;
                    m_anim.CrossFade("FieldAvaterWalkBackAnimation", 0);
                }

                break;
            case MoveState.doge:
                float dogeVecX = 0;
                float dogeVecY = 0;
                if(m_h == 0 && m_v == 0)
                {
                    switch (m_nowDirection)
                    {
                        case StandbyDirection.down:
                            dogeVecY = -1;
                            break;
                        case StandbyDirection.up:
                            dogeVecY = 1;
                            break;
                        case StandbyDirection.left:
                            dogeVecX = -1;
                            break;
                        case StandbyDirection.right:
                            dogeVecX = 1;
                            break;
                    }
                }
                else
                {
                    dogeVecX = m_h;
                    dogeVecY = m_v;
                }

                if(!m_nowDoge)
                {
                    if (dogeVecX > 0)
                    {
                        m_nowDirection = StandbyDirection.right;
                        m_anim.CrossFade("FieldAvaterDogeRightAnimation", 0);
                    }
                    else if (dogeVecX < 0)
                    {
                        m_nowDirection = StandbyDirection.left;
                        m_anim.CrossFade("FieldAvaterDogeLeftAnimation", 0);
                    }
                    else if (dogeVecY > 0)
                    {
                        m_nowDirection = StandbyDirection.up;
                        m_anim.CrossFade("FieldAvaterDogeBackAnimation", 0);
                    }
                    else if (dogeVecY < 0)
                    {
                        m_nowDirection = StandbyDirection.down;
                        m_anim.CrossFade("FieldAvaterDogeFrontAnimation", 0);
                    }
                    m_nowDoge = true;
                }

                move = new Vector2(dogeVecX, dogeVecY);
                m_rb.velocity = move * m_dogeSpeed;

                break;
        }

    }

    public void DogeEnd()
    {
        m_nowDoge = false;
        m_nowState = MoveState.stop;
        StopAnimParDirections();
        StartCoroutine(DogeInterval());
    }

    IEnumerator DogeInterval()
    {
        yield return new WaitForSeconds(m_dogeInterval);
        m_dogeIntervalFlg = false;
    }

    public void StopAnimParDirections()
    {
        if(m_nowState == MoveState.stop)
        {
            switch (m_nowDirection)
            {
                case StandbyDirection.down:
                    m_anim.CrossFade("FieldAvaterAnimation", 0);
                    break;
                case StandbyDirection.up:
                    m_anim.CrossFade("FieldAvaterIdleFrontAnimation", 0);
                    break;
                case StandbyDirection.left:
                    m_anim.CrossFade("FieldAvaterIdleLeftAnimation", 0);
                    break;
                case StandbyDirection.right:
                    m_anim.CrossFade("FieldAvaterIdleRightAnimation", 0);
                    break;
            }
        }
    }
}
