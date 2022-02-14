using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackWallBacker : MonoBehaviour
{
    [SerializeField] Animator m_back = default;
    [SerializeField] GameObject m_syucyusen = default; // 集中線パーティクル

    int m_kaihiVec = 1;

    public void Backer()
    {
        if(Attack.m_isNomalAttacked == true)
        {
            BattleManager._theTurn = BattleManager.Turn.EnemyTurn;
            Attack.m_isNomalAttacked = false;
        }

        m_back.SetBool("IsDoge", false);
        m_back.SetBool("IsApproach", false);
        m_syucyusen.SetActive(false);

    }

    private void Update()
    {
        if(QTE.isKaihi == true)
        {
            if(m_kaihiVec == 1)
            {
                m_kaihiVec = 2;
            }
            else
            {
                m_kaihiVec = 1;
            }
            m_back.SetInteger("SideStep", m_kaihiVec);
            QTE.isKaihi = false;
        }
    }

    public void ResetBySideStep()
    {
        m_back.SetInteger("SideStep", 0);
    }

}
