using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurpriseAttackEventTriggerE : MonoBehaviour
{
    public Vector2 m_targetPos;

    public bool m_onTrriger;

    [SerializeField] BattleFieldPlayerControle m_BFPC;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            m_onTrriger = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            m_targetPos = collision.transform.position;
            m_BFPC.m_surpriseAttackTargrtPos = m_targetPos;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            m_onTrriger = false;
        }
    }
}
