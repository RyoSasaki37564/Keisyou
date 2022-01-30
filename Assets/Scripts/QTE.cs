using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTE : MonoBehaviour
{
    [SerializeField] float m_damage = default;

    float m_utuwa; //いったんfloatにして、インクリメントintキャストで四捨五入する。

    public void QTEBited()
    {
        Player.Instance.Damage(m_damage, false);
        //Debug.Log(Player.Instance.m_currentHP);
        Destroy(this.gameObject);
    }

    private void OnMouseEnter()
    {
        m_utuwa = Player.Instance.m_playerLevel / 2f;
        Player.Instance.m_currentConcentlate += (int)(m_utuwa + 1);
        Player.Instance.m_currentDogePower += (int)(m_utuwa + 1);
        Destroy(this.gameObject);
    }
}
