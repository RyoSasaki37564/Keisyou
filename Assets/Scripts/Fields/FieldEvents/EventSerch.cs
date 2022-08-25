using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSerch : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "NPC")
        {
            FieldEventManager.Instance.m_nowSetEvent = collision.gameObject.GetComponent<FieldEventUnit>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (FieldEventManager.Instance.m_nowSetEvent)
        {
            FieldEventManager.Instance.m_nowSetEvent = null;
        }
    }
}
