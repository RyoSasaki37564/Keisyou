using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillofShirogane : ShinzuiSkills
{
    [SerializeField] string m_shirogane = "真髄解放・白銀";

    string m_setumei = "3ターン後、武器威力x3のダメージ";

    [SerializeField] GameObject m_platinumCannan = default; //白銀真髄攻撃演出

    public int m_turnCount = 0;

    public bool m_flg = false;

    public override void Start()
    {
        base.Start();
        m_turnCount = 0;
        m_platinumCannan.SetActive(false);
    }

    public override void UseSkill()
    {
        if (this.m_canUse == true)
        {
            ShinzuiTimeLineLoader.skillEffectsID = 3;
            base.UseSkill();
            m_turnCount++;
        }
    }

    public override void Update()
    {
        base.Update();

        if (m_flg == false && BattleManager._theTurn == BattleManager.Turn.InputTurn)
        {
            if (m_turnCount > 0)
            {
                m_turnCount++;
            }
            if (m_turnCount == 5)
            {
                //今は悪魔に魂を売る
                NineKeyInput.m_RG0Rate = 1.2f;
                m_platinumCannan.SetActive(true);
                m_turnCount = 0;
            }
            m_flg = true;
        }
        else if(m_flg == true && BattleManager._theTurn == BattleManager.Turn.PlayerTurn)
        {
            m_flg = false;
        }
    }

    public void Kakunin()
    {
        base.Panneler(m_shirogane, m_setumei, PannelingSkillKarsol.shirogane);
    }
}
