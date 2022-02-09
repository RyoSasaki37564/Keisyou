﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillofEva : ShinzuiSkills
{
    [SerializeField] string m_eva = "真髄解放・絵歯";

    [SerializeField] string m_setumei = "敵に毒状態(3 x 30s)を付与";

    public override void UseSkill()
    {
        if (this.m_canUse == true)
        {
            ShinzuiTimeLineLoader.skillEffectsID = 4;
        }
        base.UseSkill();
    }

    public void Kakunin()
    {
        base.Panneler(m_eva, m_setumei, PannelingSkillKarsol.eva);
    }
}
