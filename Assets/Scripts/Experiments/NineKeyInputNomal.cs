using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NineKeyInputNomal : MonoBehaviour
{
    Collider2D m_thisCol;

    Vector3 m_mousePosDelta;

    bool m_isIn = false;

    bool m_slustFlg = false;

    // Start is called before the first frame update
    void Start()
    {
        m_thisCol = this.gameObject.GetComponent<Collider2D>();

        TouchManager.Began += (info) =>
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider == m_thisCol)
            {
                m_thisCol.enabled = false;
                m_isIn = true;
                m_slustFlg = true;
            }
        };

        TouchManager.Moved += (info) =>
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider == m_thisCol)
            {
                m_thisCol.enabled = false;
                m_isIn = true;
            }
        };

        TouchManager.Ended += (info) =>
        {
            m_thisCol.enabled = true;
            m_isIn = false;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider == m_thisCol && m_slustFlg == true)
            {
                Debug.Log(5);
            }
            m_slustFlg = false;
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if(Input.GetMouseButton(0) && m_isIn == true)
        {
            m_isIn = false;
            m_mousePosDelta = Input.mousePosition;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        m_slustFlg = false;
    }

}
