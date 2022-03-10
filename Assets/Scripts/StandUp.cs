using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandUp : MonoBehaviour
{
    [SerializeField] GameObject m_UICanvas = default;

    [SerializeField] GameObject m_StopCanvas = default;

    /*
    [SerializeField] AudioSource m_SE = default;
    [SerializeField] AudioClip m_ongen = default;
    */
    [SerializeField] SEPlay m_SE = default;

    [SerializeField] GameObject m_parent = default;

    public void BattleStart()
    {
        m_StopCanvas.SetActive(true);
        m_UICanvas.SetActive(true);
        m_parent.SetActive(false);
        m_SE.MyPlayOneShot(); //これがなぁ。
        Debug.Log("なった");
    }
}
