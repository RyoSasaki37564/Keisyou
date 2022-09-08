using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainArmAgent : MonoBehaviour
{
    public Toryugu m_arm;
    [SerializeField] MainArmsSettingTargetManager m_MASTAM;
    [SerializeField] GameObject m_directPanel;
    [SerializeField] Button m_directionArmButton;
    [SerializeField] Button m_outArmButton;
    [SerializeField] InventryPaneler m_ip;

    public void MainArmDirectionMode()
    {
        m_directPanel.SetActive(true);
        m_MASTAM.m_nowTargetArm = m_arm;
        m_directionArmButton.onClick.RemoveAllListeners();
        m_outArmButton.onClick.RemoveAllListeners();
        m_directionArmButton.onClick.AddListener(DirectArm);
        m_outArmButton.onClick.AddListener(OutArm);
    }

    void DirectArm()
    {
        m_ip.SelectAndOpen(2);
        m_ip.m_MADM = MainDirectMode.Direct;
    }

    void OutArm()
    {
        if(PlayerDataAlfa.Instance.m_testInventry.RemoveMainArm(m_arm))
        {
            this.gameObject.SetActive(false);
            m_ip.m_mainDirectPanel.SetActive(false);
        }
    }
}
