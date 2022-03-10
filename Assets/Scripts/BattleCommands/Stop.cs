using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Stop : MonoBehaviour
{
    bool m_kirikae = false;

    [SerializeField] GameObject m_panel = default;

    public event Action<bool> OnPauseResume;

    private void Start()
    {
        m_panel.SetActive(false);
    }

    public void IchijiTeishi()
    {
        m_kirikae = !m_kirikae;

        OnPauseResume(m_kirikae);

        m_panel.SetActive(m_kirikae);
    }
}
