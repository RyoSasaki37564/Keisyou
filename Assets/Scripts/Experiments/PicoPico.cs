using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicoPico : MonoBehaviour
{
    [SerializeField] GameObject m_go;

    private void OnEnable()
    {
        m_go.SetActive(true);
    }
}
