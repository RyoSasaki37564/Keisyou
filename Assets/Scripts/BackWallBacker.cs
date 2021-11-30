﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackWallBacker : MonoBehaviour
{
    [SerializeField] Animator m_back = default;
    [SerializeField] GameObject m_syucyusen = default; // 集中線パーティクル

    public void Backer()
    {
        m_back.SetBool("IsApproach", false);
        m_syucyusen.SetActive(false);
    }
}
