using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandUp : MonoBehaviour
{
    [SerializeField] GameObject m_UICanvas = default;

    [SerializeField] AudioSource m_SE = default;
    [SerializeField] AudioClip m_ongen = default;

    [SerializeField] GameObject m_parent = default;

    public void BattleStart()
    {
        m_SE.PlayOneShot(m_ongen);
        m_UICanvas.SetActive(true);
        m_parent.SetActive(false);
        Debug.Log("なった");
    }
}
