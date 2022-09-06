﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmDataPaneler : MonoBehaviour
{
    [System.NonSerialized] public int m_id;
    [SerializeField] Text[] m_dataPanels = new Text[2];

    public void ShowData()
    {
        m_dataPanels[0].text = PlayerDataAlfa.Instance.m_testInventry.m_armsInventry[m_id].GetName;
        m_dataPanels[1].text = PlayerDataAlfa.Instance.m_testInventry.m_armsInventry[m_id].GetFravorText;

        //リサイジングさせるために必要
        m_dataPanels[1].gameObject.SetActive(false);
        m_dataPanels[1].gameObject.SetActive(true);
    }
}
