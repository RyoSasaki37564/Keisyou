using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shinzui : MonoBehaviour
{
    [SerializeField] GameObject m_pannel = default;

    [SerializeField] List<GameObject> m_otherCommands = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        m_pannel.SetActive(false);
    }

    public void Panneling()
    {
        if (m_pannel.activeSelf == true)
        {
            m_pannel.SetActive(false);
            foreach(var i in m_otherCommands)
            {
                i.SetActive(true);
            }
        }
        else
        {
            m_pannel.SetActive(true);
            foreach (var i in m_otherCommands)
            {
                i.SetActive(false);
            }
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
