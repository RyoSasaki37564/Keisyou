using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTE : MonoBehaviour
{
    [SerializeField] float m_damage = default;

    public void QTEBited()
    {
        Player.Instance.Damage(m_damage, false);
        Debug.Log(Player.Instance.m_currentHP);
        Destroy(this.gameObject);
    }

    private void OnMouseEnter()
    {
        Destroy(this.gameObject);
    }
}
