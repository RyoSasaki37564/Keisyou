using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillOfInga : ShinzuiSkills
{
    [SerializeField] string m_inga = "真髄解放・因果";

    string m_setumei = "次の攻撃を受けた時、\n自身の体力が3割以下なら龍撃";

    public override void UseSkill()
    {
        if (this.m_canUse == true)
        {
            ShinzuiTimeLineLoader.skillEffectsID = 1;
            base.UseSkill();
        }
    }

    public void Kakunin()
    {
        base.Panneler(m_inga, m_setumei, PannelingSkillKarsol.inga);
    }
}

