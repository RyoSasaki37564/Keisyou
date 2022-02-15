using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pally : MonoBehaviour
{
    [SerializeField] Enemy m_ene;

    [SerializeField] GameObject m_effects = default;

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    private void Start()
    {
        TouchManager.Began += (info) =>
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(info.screenPoint), Vector2.zero);
            if (hit.collider != null)
            {
                GameObject hitObj = hit.collider.gameObject;
                if (hitObj.tag == "Danger")
                {
                    m_effects.SetActive(true);
                    m_ene.PallyTest();
                }
            }
        };

        TouchManager.Moved += (info) =>
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(info.screenPoint), Vector2.zero);
            if (hit.collider != null)
            {
                GameObject hitObj = hit.collider.gameObject;
                if (hitObj.tag == "Danger")
                {
                    m_effects.SetActive(true);
                    m_ene.PallyTest();
                }
            }
        };
    }
}
