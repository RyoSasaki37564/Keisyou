using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainArmDamy : MonoBehaviour
{
    [SerializeField] InventryPaneler m_ip;

    public void MainArmAddMode()
    {
        m_ip.SelectAndOpen(2);
        m_ip.m_MADM = MainArmDirectMode.Add;
    }
}
