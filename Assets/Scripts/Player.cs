using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static int _pHP = 100;

    [SerializeField] Slider m_hpSlider = default;

    // Start is called before the first frame update
    void Start()
    {
        m_hpSlider.maxValue = _pHP;
        m_hpSlider.value = _pHP;
    }

    // Update is called once per frame
    void Update()
    {
        m_hpSlider.value = _pHP;
    }
}
