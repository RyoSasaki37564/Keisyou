using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMover : MonoBehaviour
{
    [SerializeField] float m_sp = 0.01f;

    [SerializeField] GameObject m_goal = default;

    [SerializeField] LineEffectMnager m_Mana = default;

    // Update is called once per frame
    void Update()
    {
        if(m_Mana.m_isTate == false)
        {
            Yoko();
        }
        else
        {
            Tate();
        }
    }

    void Yoko()
    {
        this.gameObject.transform.Translate(m_sp, 0, 0);

        if (this.transform.position.x > m_goal.transform.position.x)
        {
            this.gameObject.SetActive(false);
        }
    }

    void Tate()
    {
        {
            this.gameObject.transform.Translate(0, m_sp, 0);

            if (this.transform.position.y > m_goal.transform.position.y)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
