using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ryugeki : MonoBehaviour
{
    [SerializeField] GameObject m_nines = default; //九字鍵盤

    public static bool m_isHitRyugeki = false; //龍撃を行っているか否か

    public void RyugekiNoKamae()
    {
        if(BattleManager._theTurn == BattleManager.Turn.InputTurn ||
           Enemy.m_isRyugekiChance == true)
        {
            m_nines.SetActive(true);
        }
    }
}
