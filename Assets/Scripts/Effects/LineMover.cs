using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMover : MonoBehaviour
{
    [SerializeField] float m_sp = 0.01f;

    [SerializeField] GameObject m_goal = default;

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Translate(m_sp, 0, 0);

        if(this.transform.position.x > m_goal.transform.position.x)
        {
            this.gameObject.SetActive(false);
        }
    }
}
