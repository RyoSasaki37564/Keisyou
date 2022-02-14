using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackWallBacker : MonoBehaviour
{
    [SerializeField] Animator m_back = default;
    [SerializeField] GameObject m_syucyusen = default; // 集中線パーティクル

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
            int rand = (int)Random.Range(1, 2.9f);
            m_back.SetInteger("SideStep", rand);
            QTE.isKaihi = false;
        }
    }

    public void ResetBySideStep()
    {
        m_back.SetInteger("SideStep", 0);
    }

}
