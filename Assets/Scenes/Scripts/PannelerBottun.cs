using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PannelerBottun : MonoBehaviour
{
    [SerializeField] GameObject m_pannel = default; 

    // Start is called before the first frame update
    void Start()
    {
        m_pannel.SetActive(false);
    }

    public void Panneling()
    {
        if(m_pannel.activeSelf == true)
        {
            m_pannel.SetActive(false);
        }
        else
        {
            m_pannel.SetActive(true);
        }
    }

    public void PannelOpen()
    {
        m_pannel.SetActive(true);
    }

    public void PannelClose()
    {
        m_pannel.SetActive(false);
    }

}
