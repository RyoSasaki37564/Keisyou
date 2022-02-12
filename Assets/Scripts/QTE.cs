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
        Player.Instance.Damage(EnemyStuts.m_enemiesStuts[Target.m_tergetNum].m_attack, false);
        Destroy(this.gameObject);
    }

    private void Start()
    {
        TouchManager.Began += (info) =>
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(info.screenPoint), Vector2.zero);
            if (hit.collider != null)
            {
                GameObject hitObj = hit.collider.gameObject;
                //ヒットしたオブジェクトのタグがQTEだったら消して集中と回避を付与 
                if (hitObj.tag == "QTE")
                {
                    Destroy(hitObj.gameObject);
                    m_utuwa = Player.Instance.m_playerLevel / 2f;
                    Player.Instance.m_currentConcentlate += (int)(m_utuwa + 1);
                    Player.Instance.m_currentDogePower += (int)(m_utuwa + 1);
                }
            }
        };

        TouchManager.Moved += (info) =>
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(info.screenPoint), Vector2.zero);
            if (hit.collider != null)
            {
                GameObject hitObj = hit.collider.gameObject;
                //ヒットしたオブジェクトのタグがQTEだったら消して集中と回避を付与 
                if (hitObj.tag == "QTE")
                {
                    Destroy(hitObj.gameObject);
                    m_utuwa = Player.Instance.m_playerLevel / 2f;
                    Player.Instance.m_currentConcentlate += (int)(m_utuwa + 1);
                    Player.Instance.m_currentDogePower += (int)(m_utuwa + 1);
                }
            }
        };
    }
}
