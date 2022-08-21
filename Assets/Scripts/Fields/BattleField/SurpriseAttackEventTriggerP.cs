using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurpriseAttackEventTriggerP : MonoBehaviour
{
    [SerializeField] SurpriseAttackEventTriggerE m_pare;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")// && m_pare.m_onTrriger)
        {
            Debug.Log("奇襲可能");
            if (Mathf.Abs(collision.transform.position.x - m_pare.m_targetPos.x) < 1.5f)
            {
            }
        }
    }
}
