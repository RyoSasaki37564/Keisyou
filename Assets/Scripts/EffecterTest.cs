﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffecterTest : MonoBehaviour
{
    [SerializeField] float m_sp = 0.01f; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Translate(m_sp, 0, 0);
    }
}
