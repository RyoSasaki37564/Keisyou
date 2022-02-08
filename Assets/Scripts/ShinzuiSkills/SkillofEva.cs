using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillofEva : ShinzuiSkills
{
    [SerializeField] string m_eva = "真髄解放・絵歯";

    [SerializeField] string m_setumei = "このターンにこの武器の攻撃が成功した時、毒状態(3 x 30s)を付与";

    public override void UseSkill()
    {
        base.UseSkill();
    }

    public void Kakunin()
    {
        base.Panneler(m_eva, m_setumei);
    }
}
