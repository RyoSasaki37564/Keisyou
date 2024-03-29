﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] GameObject m_circle = default;

    [SerializeField] List<GameObject> m_otherCommands = new List<GameObject>(); //いま回避ボタンしか入っとらん

    [SerializeField] Animator m_ainm = default;

    [SerializeField] GameObject m_setumei = default; //内容説明のやつ 

    public bool m_dashiireFlg = false;

    [SerializeField] Shinzui m_shinzui = default;

    [SerializeField] SEPlay[] m_SEs = new SEPlay[2];

    // Start is called before the first frame update
    void Start()
    {
        m_circle.SetActive(false);
    }

    public void Panneling()
    {
        if (m_dashiireFlg == true)
        {
            m_SEs[1].MyPlayOneShot();
            m_setumei.SetActive(false);
            m_ainm.SetBool("IsItem", false);
            foreach (var i in m_otherCommands)
            {
                i.SetActive(true);
            }
            m_dashiireFlg = false;
        }
        else
        {
            m_SEs[0].MyPlayOneShot();
            m_circle.SetActive(true);
            m_dashiireFlg = true;
            m_ainm.SetBool("IsItem", true);
            foreach (var i in m_otherCommands)
            {
                i.SetActive(false);
            }
        }

        if (m_shinzui.m_dashiireFlg == true)
        {
            m_shinzui.Panneling();
        }
    }
}
