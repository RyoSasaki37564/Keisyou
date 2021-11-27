﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance = default;

    public int m_pHP = 100;

    public float m_maxConcentlate = 100;
    public float m_concentlate = 0;

    public float m_dogePower = 100;

    [SerializeField] Slider m_hpSlider = default;
    [SerializeField] Slider m_conSlider = default;
    [SerializeField] Slider m_dgSlider = default;

    private void Awake()
    {
        if(Instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_hpSlider.maxValue = m_pHP;
        m_hpSlider.value = m_pHP;

        m_conSlider.maxValue = m_maxConcentlate;
        m_conSlider.value = m_concentlate;

        m_dgSlider.maxValue = m_dogePower;
        m_dgSlider.value = m_dogePower;

    }

    // Update is called once per frame
    void Update()
    {
        m_hpSlider.value = m_pHP;
        m_conSlider.value = m_concentlate;
        m_dgSlider.value = m_dogePower;
    }
}
