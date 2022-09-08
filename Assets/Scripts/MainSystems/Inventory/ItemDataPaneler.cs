using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDataPaneler : MonoBehaviour
{
    [System.NonSerialized] public int m_id;
    [SerializeField] Text[] m_dataPanels = new Text[2];
    public InventryPaneler m_ip;

    public void ShowData()
    {
        m_dataPanels[0].text = PlayerDataAlfa.Instance.m_testInventry.m_itemInventry[m_id].GetName;
        m_dataPanels[1].text = PlayerDataAlfa.Instance.m_testInventry.m_itemInventry[m_id].GetFravorText;

        //リサイジングさせるために必要
        m_dataPanels[1].gameObject.SetActive(false);
        m_dataPanels[1].gameObject.SetActive(true);
    }

    public void IntoShortCut()
    {
        if (m_ip.m_MADM == MainDirectMode.Direct)
        {
            PlayerDataAlfa.Instance.m_testInventry.ReplaceItem(m_ip.GetMASTM().m_nowTargetItem.GetID,
                   PlayerDataAlfa.Instance.m_testInventry.m_itemInventry[m_id].GetID, m_ip.m_targetShortCutSlot);
            m_ip.SelectAndOpen(0);
        }
        else if (m_ip.m_MADM == MainDirectMode.Add)
        {
            PlayerDataAlfa.Instance.m_testInventry.IntoShortCut(PlayerDataAlfa.Instance.m_testInventry.m_itemInventry[m_id].GetID, m_ip.m_targetShortCutSlot);
            m_ip.SelectAndOpen(0);
        }
    }
}
