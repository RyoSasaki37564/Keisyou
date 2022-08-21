using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class BattleFieldPlayerControle : MonoBehaviour
{
    Animator m_anim;
    Rigidbody2D m_rb;
    Collider2D m_col;

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

    float m_stamina = 20f;
    bool m_cantHealigStamina;
    bool m_cantUseStamina;
    [SerializeField] float m_dashCostStamina = 0.2f;
    [SerializeField] float m_dogeCostStamina = 5f;
    [SerializeField] float m_staminaHealpoint = 0.1f;

    Slider m_staminaBar;

    public Vector3 m_surpriseAttackTargrtPos;

    // Start is called before the first frame update
    void Start()
    {
        m_staminaBar = GameObject.Find("StaminaSlider").GetComponent<Slider>();
        m_staminaBar.maxValue = m_stamina;
        m_staminaBar.value = m_stamina;
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
        m_col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_stamina < m_staminaBar.maxValue && !m_cantHealigStamina && !m_dogeIntervalFlg)
        {
            m_stamina += m_staminaHealpoint;
            m_staminaBar.value = m_stamina;
            if (m_stamina >= m_staminaBar.maxValue)
            {
                m_stamina = m_staminaBar.maxValue;
                m_staminaBar.value = m_stamina;
                m_cantUseStamina = false;
            }
        }

        bool dash = false;

        if(m_nowState == MoveState.stop || m_nowState == MoveState.walk)
        {
            m_h = Input.GetAxisRaw("Horizontal");
            m_v = Input.GetAxisRaw("Vertical");
            bool doge = Input.GetKeyDown(KeyCode.LeftControl);

            if(!m_cantUseStamina)
            {
                dash = Input.GetKey(KeyCode.LeftShift);
            }
            
            if (!m_dogeIntervalFlg && doge && (m_nowState == MoveState.stop || m_nowState == MoveState.walk))
            {
                if(!m_cantUseStamina)
                {
                    m_nowState = MoveState.doge;
                    m_dogeIntervalFlg = true;
                }
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
                m_cantHealigStamina = false;
                m_rb.velocity = Vector2.zero;
                break;

            case MoveState.walk:
                Vector2 move = new Vector2(m_h, m_v);
                move.Normalize();
                float speed = m_speedRate * (dash ? m_dashRate : 1);
                m_rb.velocity = move * speed;

                if(dash)
                {
                    m_cantHealigStamina = true;
                    m_stamina -= m_dashCostStamina;
                    if(m_stamina < 0)
                    {
                        m_stamina = 0;
                        m_cantUseStamina = true;
                    }
                    m_staminaBar.value = m_stamina;
                }
                else
                {
                    m_cantHealigStamina = false;
                }

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
                m_cantHealigStamina = true;
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

                if(!m_nowDoge && m_stamina >= 0)
                {
                    m_stamina -= m_dogeCostStamina;
                    if(m_stamina < 0)
                    {
                        m_stamina = 0;
                        m_cantUseStamina = true;
                    }
                    m_staminaBar.value = m_stamina;
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

            case MoveState.dropAttack:
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

    public void Kishu()
    {
        m_col.isTrigger = true;
        m_nowState = MoveState.dropAttack;
        m_anim.CrossFade("FieldAvaterDropAttackAnimation", 0);
        transform.DOLocalMove(m_surpriseAttackTargrtPos/transform.localScale.x, 1f);//なぜかスケール倍率を受けてゴールがずれるので、スケールで割るとうまくいく。

    }
}
