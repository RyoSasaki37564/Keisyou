using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurpriseAttackEventTriggerP : MonoBehaviour
{
    [SerializeField] SurpriseAttackEventTriggerE m_pare;

    [SerializeField] GameObject m_kishuBottun;

    private void OnTriggerEnter2D(Collider2D collision)
    {
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerAvater" && m_pare.m_onTrriger)
        {
            if (Mathf.Abs(collision.transform.position.x - m_pare.m_targetPos.x) < 1.5f)
            {
                if(!m_kishuBottun.activeSelf)
                {
                    m_kishuBottun.SetActive(true);
                }
            }
            else
            {
                if (m_kishuBottun.activeSelf)
                {
                    m_kishuBottun.SetActive(false);
                }
            }
        }
        else
        {
            if (m_kishuBottun.activeSelf)
            {
                m_kishuBottun.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerAvater")
        {
            if (m_kishuBottun.activeSelf)
            {
                m_kishuBottun.SetActive(false);
            }
        }
    }
}
