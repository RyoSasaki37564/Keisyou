using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillOfJiga : ShinzuiSkills
{
    [SerializeField] string m_jiga = "真髄解放・自我";

    [SerializeField] string m_setumei = "次に受ける攻撃を回避する";
    
    public bool m_zettaiKaihi = false;

    public override void UseSkill()
    {
        if (this.m_canUse == true)
        {
            m_zettaiKaihi = true;
            ShinzuiTimeLineLoader.skillEffectsID = 0;
        }
        base.UseSkill();
    }

    public void Kakunin()
    {
        base.Panneler(m_jiga, m_setumei, PannelingSkillKarsol.jiga);
    }
}
