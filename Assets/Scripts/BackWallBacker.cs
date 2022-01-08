using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackWallBacker : MonoBehaviour
{
    [SerializeField] Animator m_back = default;
    [SerializeField] GameObject m_syucyusen = default; // 集中線パーティクル

    public void Backer()
    {
        BattleManager._theTurn = BattleManager.Turn.EnemyTurn;
        Debug.LogWarning("Hey");

        m_back.SetBool("IsDoge", false);
        m_back.SetBool("IsApproach", false);
        m_syucyusen.SetActive(false);

    }
}
