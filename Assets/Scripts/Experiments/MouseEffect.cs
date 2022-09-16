using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEffect : MonoBehaviour
{
    Vector3 m_position;
    Vector3 m_screenToWorldPointPosition;

    [SerializeField] GameObject m_hitEffectTemp;

    [SerializeField] Transform m_poolPos;

    int m_indexer = 1;

    [SerializeField] BatteManagerAlfa m_bma;

    private void Start()
    {
        for(var i = 0; i < 50; i++)
        {
            var x = Instantiate(m_hitEffectTemp, m_poolPos);
            x.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_STANDALONE || UNITY_WEBGL || IS_EDITOR

        m_position = Input.mousePosition;

        if(m_bma.GetModeOfZone == ModeOfZone.Kamigakari && m_bma.GetNowZone && Input.GetMouseButtonDown(0))
        {
            KamigakariHitEffect();
        }

#elif UNITY_ANDROID || UNITY_ANDROID_API

#endif
        m_position.z = 10f;
        m_screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(m_position);
        gameObject.transform.position = m_screenToWorldPointPosition;
    }

    void KamigakariHitEffect()
    {
        var x = m_poolPos.GetChild(m_indexer).gameObject;
        x.transform.position = m_screenToWorldPointPosition;
        x.SetActive(true);
        m_indexer++;
        if (m_indexer >= m_poolPos.childCount)
        {
            m_indexer = 1;
        }
    }
}
