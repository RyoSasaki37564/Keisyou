using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    float _power = 10;

    public void AttackAct()
    {
        BattleManager.Instance.m_currentEnemyHP -= 10;
        Debug.LogWarning(BattleManager.Instance.m_currentEnemyHP);
        BattleManager.Instance.TurnAdvance();
        Debug.Log("攻撃");
    }
}
