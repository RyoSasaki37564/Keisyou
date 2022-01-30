﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ryugeki : MonoBehaviour
{
    [SerializeField] GameObject m_nines = default; //九字鍵盤

    [SerializeField] Text m_dialog = default; //ダイアログ

    [SerializeField] List<GameObject> m_playerCommandsUI = new List<GameObject>(); //攻撃や回避などの龍撃以外のボタン系UIの親オブジェクト

    public static bool m_isHitRyugeki = false; //龍撃を行っているか否か

    [SerializeField] NineKeyInput m_nineKeysScript = default;

    [SerializeField] Image m_fader = default; //フェードインアウト用スクリーン

    [SerializeField] Animator m_approchTobackWall = default; //接近アニメーション
    [SerializeField] GameObject m_syucyusen = default; // 集中線パーティクル


    [SerializeField] Image m_ryugekiBottun = default; //龍撃ボタン1
    [SerializeField] GameObject m_ryugekiButubutu = default; //龍撃ボタン2

    public void RyugekiNoKamae()
    {
        if(m_isHitRyugeki == true)
        {
            m_approchTobackWall.SetBool("IsApproach", true);
            StartCoroutine(FadeIn());
            m_nineKeysScript.Phaser();
            Debug.Log("龍撃撃った " + BattleManager._theTurn);
            foreach (var i in m_playerCommandsUI)
            {
                i.SetActive(true);
            }
            m_nines.SetActive(false);
            m_ryugekiBottun.color = new Color(255, 255, 255, 0);
            m_ryugekiButubutu.SetActive(false);
            m_isHitRyugeki = false;
        }
        else
        {
            if(Enemy.m_isRyugekiChance == true)
            {
                m_syucyusen.SetActive(true);
                StartCoroutine(FadeIn());
                BattleManager._theTurn = BattleManager.Turn.PlayerTurn;
                m_isHitRyugeki = true;
                foreach (var i in m_playerCommandsUI)
                {
                    i.SetActive(false);
                }
                m_nines.SetActive(true);
                m_dialog.text = "";
            }
        }

    }

    IEnumerator FadeIn()
    {
        m_fader.color = new Color(250, 250, 250, 250);
        yield return new WaitForSeconds(0.15f);
        m_fader.color = new Color(0, 0, 0, 0);
        BattleManager._theTurn = BattleManager.Turn.InputTurn;
    }
}
