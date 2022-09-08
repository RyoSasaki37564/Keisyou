using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//装備追加用装置
public class MainArmDamy : MonoBehaviour
{
    [SerializeField] InventryPaneler m_ip;

    public void MainArmAddMode()
    {
        m_ip.SelectAndOpen(2);
        m_ip.m_MADM = MainDirectMode.Add;
    }
}
