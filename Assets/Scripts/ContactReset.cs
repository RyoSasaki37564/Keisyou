using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactReset : MonoBehaviour
{
    [SerializeField] GameObject[] m_commands = new GameObject[9];

    public void Resetter()
    {
        foreach(var i in m_commands)
        {
            i.SetActive(true);
        }
    }
}
