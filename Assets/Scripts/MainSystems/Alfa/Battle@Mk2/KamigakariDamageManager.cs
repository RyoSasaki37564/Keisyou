using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ターゲットセッティング装置も兼ねる
/// </summary>
public class KamigakariDamageManager : MonoBehaviour
{
    public int m_id = 0;

    public Slider m_hpSlider;

    float m_doTime = 0.1f;
    
    public Transform m_cameraTF;

    public BatteManagerAlfa m_bma;

    // Start is called before the first frame update
    void Start()
    {
        TouchManager.Began += (info) =>
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if(hit)
            {
                if (hit.collider.transform.root == transform)
                {
                    if (m_bma.GetTarget != m_id)
                    {
                        int pTarget = m_bma.GetTarget;
                        m_bma.SetTarget = m_id;
                        m_bma.ChangeTarget(pTarget, m_bma.GetTarget);
                    }

                    if (m_bma.GetModeOfZone == ModeOfZone.Kamigakari && m_bma.GetNowZone)
                    {
                        KamigakariDamage();
                    }
                }
            }
        };
    }

    public void KamigakariDamage()
    {
        StartCoroutine(Shake(m_doTime, 0.6f)); //画面揺らし
        m_hpSlider.value -= m_hpSlider.maxValue / 100;
        if(m_hpSlider.value <= 0)
        {
            m_bma.IsEnemyDead();
        }
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = m_cameraTF.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            m_cameraTF.position = originalPosition + Random.insideUnitSphere * magnitude;
            elapsed += Time.deltaTime;
            yield return null;
        }
        m_cameraTF.position = originalPosition;
    }
}
