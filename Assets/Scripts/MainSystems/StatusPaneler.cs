﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusPaneler : MonoBehaviour
{
    [SerializeField] Text[] m_stastusText = new Text[10];

    public void ShowStatus()
    {
        m_stastusText[0].text = "レベル:" + PlayerDataAlfa.Instance.GetStuts.m_level.ToString();
        m_stastusText[1].text = "体力:" + PlayerDataAlfa.Instance.GetStuts.m_hp.ToString();
        m_stastusText[2].text = "攻撃:" + PlayerDataAlfa.Instance.GetStuts.m_atk.ToString();
        m_stastusText[3].text = "防御:" + PlayerDataAlfa.Instance.GetStuts.m_def.ToString();
        m_stastusText[4].text = "集中:" + PlayerDataAlfa.Instance.GetStuts.m_con.ToString();
        m_stastusText[5].text = "回避:" + PlayerDataAlfa.Instance.GetStuts.m_dodge.ToString();
        m_stastusText[6].text = "覚悟:" + PlayerDataAlfa.Instance.GetKakugo.ToString();
        m_stastusText[7].text = "経験値:" + PlayerDataAlfa.Instance.m_exp.ToString();
        m_stastusText[8].text = "所持金:" + PlayerDataAlfa.Instance.m_money.ToString();
        m_stastusText[9].text = "技量:" + PlayerDataAlfa.Instance.m_tp.ToString();
    }
}
