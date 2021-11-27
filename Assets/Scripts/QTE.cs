using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTE : MonoBehaviour
{
    
    public void QTEBited()
    {
        Player.Instance.m_pHP -= 10;
        Destroy(this.gameObject);
    }

    private void OnMouseEnter()
    {
        Destroy(this.gameObject);
    }
}
