using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillofShirogane : ShinzuiSkills
{
    [SerializeField] string m_shirogane = "真髄解放・白銀";

    [SerializeField] string m_setumei = "このターン攻撃できなくなるが、次のターンこの武器の威力の3倍のダメージを与える。";

    public override void UseSkill()
    {
        base.UseSkill();
    }

    public void Kakunin()
    {
        base.Panneler(m_shirogane, m_setumei);
    }
}
