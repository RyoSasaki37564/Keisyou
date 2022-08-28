using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PannelerBottun : MonoBehaviour
{
    [SerializeField] GameObject[] m_pannels = new GameObject[3];

    // Start is called before the first frame update
    void Start()
    {
        AllPannelClose();
    }

    public void MenuPanneling()
    {
        if (m_pannels[0].activeSelf == false)
        {
            m_pannels[0].SetActive(true);
        }
        else
        {
            AllPannelClose();
        }
    }

    public void PannelOpen(int num)
    {
        m_pannels[num].SetActive(true);
    }

    public void PannelClose(int num)
    {
        m_pannels[num].SetActive(false);
    }

    public void AllPannelClose()
    {
        foreach (var p in m_pannels)
        {
            if(p.activeSelf)
            {
                p.SetActive(false);
            }
        }
    }
}
