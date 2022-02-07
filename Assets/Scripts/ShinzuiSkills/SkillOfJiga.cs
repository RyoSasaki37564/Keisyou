using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillOfJiga : ShinzuiSkills
{
    [SerializeField] string m_jiga = "真髄解放・自我";

    [SerializeField] string m_setumei = "次に受ける攻撃を回避する。";
    
    public bool m_zettaiKaihi = false;

    public override void UseSkill()
    {
        base.UseSkill();
        m_zettaiKaihi = true;
    }

    public override void Panneler(string name, string setumei)
    {
        base.Panneler(name, setumei);
    }

    public void Kakunin()
    {
        Panneler(m_jiga, m_setumei);
    }
}
