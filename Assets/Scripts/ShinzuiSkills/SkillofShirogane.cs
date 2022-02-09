using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillofShirogane : ShinzuiSkills
{
    [SerializeField] string m_shirogane = "真髄解放・白銀";

    string m_setumei = "このターン攻撃できなくなり、\n次のターン武器威力x3のダメージ";

    public override void UseSkill()
    {
        if (this.m_canUse == true)
        {
            ShinzuiTimeLineLoader.skillEffectsID = 3;
        }
        base.UseSkill();
    }

    public void Kakunin()
    {
        base.Panneler(m_shirogane, m_setumei, PannelingSkillKarsol.shirogane);
    }
}
