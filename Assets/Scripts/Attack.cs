using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]float m_power = 10;

    Player m_p = default;

    [SerializeField] Animator m_approchTobackWall = default; //接近アニメーション

    [SerializeField] GameObject m_syucyusen = default; // 集中線パーティクル

    [SerializeField] Target m_tergetIndexer = default; //ターゲット番号

    private void Start()
    {
        m_p = Player.Instance;
        m_syucyusen.SetActive(false);
    }

    public void AttackAct()
    {
        if(BattleManager._theTurn == BattleManager.Turn.InputTurn)
        {
            m_approchTobackWall.SetBool("IsApproach", true);
            m_syucyusen.SetActive(true);
            Debug.Log("攻撃");
            BattleManager.TurnAdvance();

            BattleManager.m_enemies[m_tergetIndexer.m_tergetNum].Damage(m_power, false); //標的に対して通常攻撃

            //UI反映
            BattleManager.m_enemies[m_tergetIndexer.m_tergetNum].m_enemyHPSL.value = BattleManager.m_enemies[m_tergetIndexer.m_tergetNum].m_currentHP;

            Debug.Log("現在の敵体力" + BattleManager.m_enemies[m_tergetIndexer.m_tergetNum].m_currentHP);

            //残り回避率に応じて集中力を増加
            if(m_p.m_currentDogePower > 0)
            {
                m_p.m_currentConcentlate += m_p.m_currentDogePower / m_p.m_dogePowerMax;
            }

            //攻撃するたび回避率を減少
            if(m_p.m_currentDogePower >= 5)
            {
                m_p.m_currentDogePower -= 5;
            }
            else
            {
                m_p.m_currentDogePower = 0;
            }
        }
        else
        {
            Debug.Log("今は攻撃できましぇん");
        }
    }
}
