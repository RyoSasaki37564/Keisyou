using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShortcutAgent : MonoBehaviour
{
    public ItemData m_shortCutTarget;
    [SerializeField] GameObject m_directPanel;
    [SerializeField] Button m_directionShortcutButton;
    [SerializeField] Button m_outShortcutButton;
    [SerializeField] InventryPaneler m_ip;
    [SerializeField] MainArmsSettingTargetManager m_MASTAM;
    [SerializeField] public int m_mySlotNumber;

    public void ShortCutDirectMode()
    {
        m_ip.m_targetShortCutSlot = m_mySlotNumber;
        m_directPanel.SetActive(true);
        m_directionShortcutButton.onClick.RemoveAllListeners();
        m_outShortcutButton.onClick.RemoveAllListeners();
        m_directionShortcutButton.onClick.AddListener(DirectItem);
        m_outShortcutButton.onClick.AddListener(OutShortCut);
        if(m_shortCutTarget == null)
        {
            m_ip.SelectAndOpen(1);
            m_ip.m_MADM = MainDirectMode.Add;
        }
        else
        {
            m_MASTAM.m_nowTargetItem = m_shortCutTarget;
        }
    }
    void DirectItem()
    {
        m_ip.SelectAndOpen(1);
        m_ip.m_MADM = MainDirectMode.Direct;
    }

    void OutShortCut()
    {
        PlayerDataAlfa.Instance.m_testInventry.RemovefromShortCut(m_shortCutTarget.GetID, m_mySlotNumber);
        m_ip.ShortCutView();
        m_ip.m_mainDirectPanel.SetActive(false);
    }
}
