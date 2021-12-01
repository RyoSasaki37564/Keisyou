using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]float m_power = 10;

    Player m_p = default;

    [SerializeField] Animator m_approchTobackWall = default; //接近アニメーション

    [SerializeField] GameObject m_syucyusen = default; // 集中線パーティクル

    private void Start()
    {
        m_p = Player.Instance;
        m_syucyusen.SetActive(false);
    }

    public void AttackAct()
    {
        if(BattleManager.theTurn == BattleManager.Turn.InputTurn)
        {
            m_approchTobackWall.SetBool("IsApproach", true);
            m_syucyusen.SetActive(true);
            Debug.Log("攻撃");
            BattleManager.TurnAdvance();
            m_p.m_currentConcentlate++;
        }
        else
        {
            Debug.Log("今は攻撃できましぇん");
        }
    }
}
