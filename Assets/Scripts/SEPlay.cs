using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class SEPlay : MonoBehaviour
{
    GameObject m_seObj = default;

    // Start is called before the first frame update
    void Awake()
    {
        gameObject.SetActive(false);
    }

    public void MyPlayOneShot()
    {
        m_seObj.SetActive(false);
        m_seObj.SetActive(true);
    }
}
