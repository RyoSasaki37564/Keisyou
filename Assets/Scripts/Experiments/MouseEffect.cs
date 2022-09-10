using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEffect : MonoBehaviour
{
    Vector3 m_position;
    Vector3 m_screenToWorldPointPosition;

    // Update is called once per frame
    void Update()
    {
#if UNITY_STANDALONE || UNITY_WEBGL || IS_EDITOR
        m_position = Input.mousePosition;
        m_position.z = 10f;
        m_screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(m_position);
        gameObject.transform.position = m_screenToWorldPointPosition;
#endif
    }
}
