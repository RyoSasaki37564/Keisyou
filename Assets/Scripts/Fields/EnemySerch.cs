using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SerchPhase
{
    NonLock,
    Coution,
    LockON,
}

public class EnemySerch : MonoBehaviour
{
    [SerializeField] FieldEnemyMoveController m_FEMC;

    [SerializeField] float m_shiya = 100f;

    void Serching(Collider2D col)
    {
        if (col.gameObject.tag == "PlayerAvater")
        {
            switch (m_FEMC.m_nowDirection)
            {
                case StandbyDirection.down:
                    Vector3 posDelta = col.transform.position - transform.position;
                    float targetAngle = Vector3.Angle(Vector3.down, posDelta);
                    if (targetAngle < m_shiya)
                    {
                        Debug.Log("視界の範囲内＆視界の角度内");
                    }
                    else
                    {

                    }
                    break;
                case StandbyDirection.up:
                    break;
                case StandbyDirection.left:
                    break;
                case StandbyDirection.right:
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Serching(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Serching(collision);
    }
}
