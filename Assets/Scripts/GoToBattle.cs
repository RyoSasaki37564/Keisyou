﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToBattle : MonoBehaviour
{
    [SerializeField] SEPlay m_SEs = default;

    [SerializeField] GameObject m_fadeOut = default;

    private void Start()
    {
        m_fadeOut.SetActive(false);
    }

    public void BattleStart()
    {
        m_fadeOut.SetActive(true);
        m_SEs.MyPlayOneShot();
        StartCoroutine(BattlePhase());
    }

    IEnumerator BattlePhase()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("Battle");
    }

}
