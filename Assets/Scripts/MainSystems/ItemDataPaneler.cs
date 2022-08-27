using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDataPaneler : MonoBehaviour
{
    [System.NonSerialized] public int m_id;
    [SerializeField] Text[] m_dataPanels = new Text[2];

    public void ShowData()
    {
        m_dataPanels[0].text = PlayerDataAlfa.Instance.m_testInventry.m_itemInventry[m_id].GetName;
        m_dataPanels[1].text = PlayerDataAlfa.Instance.m_testInventry.m_itemInventry[m_id].GetFravorText;
    }
}
