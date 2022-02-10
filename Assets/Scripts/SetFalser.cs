using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFalser : MonoBehaviour
{
    [SerializeField] List<GameObject> m_targets = new List<GameObject>();

    public void TargetsAllOff()
    {
        foreach(var i in m_targets)
        {
            i.SetActive(false);
        }
    }
}
