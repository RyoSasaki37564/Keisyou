using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]float m_power = 10;

    Player m_p = default;

    [SerializeField] Animator m_approchTobackWall = default; //接近アニメーション

    private void Start()
    {
        m_p = Player.Instance;
    }

    public void AttackAct()
    {
        if(BattleManager.theTurn == BattleManager.Turn.InputTurn)
        {
            m_approchTobackWall.SetBool("IsApproach", true);
            Debug.Log("攻撃");
            BattleManager.TurnAdvance();
            m_p.m_concentlate++;
        }
        else
        {
            Debug.Log("今は攻撃できましぇん");
        }
    }
}
