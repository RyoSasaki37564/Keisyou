﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillOfSyokeisya : ShinzuiSkills
{
    [SerializeField] string m_syokeisya = "真髄解放・処刑者";

    string m_setumei = "回避率を全消費\n次の攻撃の威力を2倍にする";

    public override void UseSkill()
    {
        if (this.m_canUse == true)
        {
            ShinzuiTimeLineLoader.skillEffectsID = 2;
            base.UseSkill();
        }
    }

    public void Kakunin()
    {
        base.Panneler(m_syokeisya, m_setumei, PannelingSkillKarsol.syokeisya);
    }
}
