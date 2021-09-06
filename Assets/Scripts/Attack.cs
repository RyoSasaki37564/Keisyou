using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    int m_playerAttackPower = 100; //プレイヤー攻撃力

    [SerializeField]
    Enemy enemyBody = default;

    // Start is called before the first frame update
    public void PlayerAttack()
    {
        enemyBody.m_enemyCurrentHp -= m_playerAttackPower;
        enemyBody.m_enemyDamageFlag = true;
    }

}
