using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NineKeyInputNomal : MonoBehaviour
{
    Collider2D m_thisCol;

    Vector3 m_mousePosDelta;

    bool m_isIn = false;
    bool m_isInStopper = false;

    bool m_slustFlg = false;

    bool m_zangekiFlg = false;

    float m_zangekiDirection; //斬撃角度

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
                //m_thisCol.enabled = false;
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
                //m_thisCol.enabled = false;
                if(m_isInStopper == false)
                {
                    m_isIn = true;
                    m_isInStopper = true;
                }
            }
        };

        TouchManager.Ended += (info) =>
        {
            m_isIn = false;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider == m_thisCol && m_slustFlg == true)
            {
                Debug.Log(5);
            }
            else if (m_zangekiFlg == true)
            {
                var heading = Input.mousePosition - m_mousePosDelta;
                var distance = heading.magnitude;
                var direction = heading / distance;
                m_zangekiDirection = Mathf.Atan2(direction.y, direction.x) * (180 / Mathf.PI);
                Debug.Log(Mathf.Repeat(m_zangekiDirection, 360) + "うち");
            }
            m_slustFlg = false;

            m_zangekiFlg = false;

            m_isInStopper = false;

            m_thisCol.enabled = false;

            m_thisCol.enabled = true;
        };
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButton(0) && m_isIn == true)
        {
            Debug.LogWarning("入ったー");
            m_isIn = false;
            m_mousePosDelta = Input.mousePosition;
            m_zangekiFlg = true;
        }
    }

    private void OnMouseExit()
    {
        if (m_zangekiFlg == true)
        {
            var heading = Input.mousePosition - m_mousePosDelta;
            var distance = heading.magnitude;
            var direction = heading / distance;
            m_zangekiDirection = Mathf.Atan2(direction.y, direction.x) * (180 / Mathf.PI);
            Debug.Log(Mathf.Repeat(m_zangekiDirection, 360) + "ぬけ");

            m_zangekiFlg = false;

            m_isInStopper = false;

            m_thisCol.enabled = false;

            m_thisCol.enabled = true;
        }
    }

    /*
    int ZangekiDirection()
    {
        var heading = Input.mousePosition - m_mousePosDelta;
        var distance = heading.magnitude;
        var direction = heading / distance;
        m_zangekiDirection = Mathf.Repeat(Mathf.Atan2(direction.y, direction.x) * (180 / Mathf.PI), 360);
        if()
    }*/
}
