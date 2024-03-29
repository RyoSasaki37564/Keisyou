﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillofEva : ShinzuiSkills
{
    [SerializeField] string m_eva = "真髄解放・絵歯";

    string m_setumei = "敵に毒状態(3 x 10s)を付与";

    bool m_dokusu = false;

    int m_sCount = 0;
    float m_time = 0;

    public override void UseSkill()
    {
        if (this.m_canUse == true)
        {
            ShinzuiTimeLineLoader.skillEffectsID = 4;
            base.UseSkill();
            m_dokusu = true;
        }
    }

    public void Kakunin()
    {
        base.Panneler(m_eva, m_setumei, PannelingSkillKarsol.eva);
    }

    public override void Update()
    {
        base.Update();

        if(m_dokusu == true)
        {
            m_time += Time.deltaTime;
            if (m_time >= 1)
            {
                float x = 3f;
                m_sCount++;
                EnemyStuts.m_enemiesStuts[Target.m_tergetNum].Damage(x, true);
                Enemy.m_enemies[Target.m_tergetNum].m_enemyHPSL.value = Enemy.m_enemies[Target.m_tergetNum].m_enemyHPSL.value -= x;
                m_time = 0;
            }

            if(m_sCount == 10)
            {
                m_dokusu = false;
            }
        }
    }
}
