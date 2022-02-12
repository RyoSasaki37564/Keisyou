using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poisonics : MonoBehaviour
{
    int m_sCount = 0;
    float m_time = 0;

    public  void Update()
    {
        m_time += Time.deltaTime;
        if (m_time >= 1)
        {
            float x = 2f;
            m_sCount++;
            EnemyStuts.m_enemiesStuts[Target.m_tergetNum].Damage(x, true);
            Enemy.m_enemies[Target.m_tergetNum].m_enemyHPSL.value = Enemy.m_enemies[Target.m_tergetNum].m_enemyHPSL.value -= x;
            m_time = 0;
        }

        if (m_sCount == 10)
        {
            Destroy(gameObject);
        }

    }
}
