using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ShinzuiSkills : MonoBehaviour
{
    [SerializeField] GameObject m_skillPannel = default; //スキル状態表示パネル
    [SerializeField] Text m_skillName = default;
    [SerializeField] Text m_skillSetumei = default;
    [SerializeField] Text m_skillCoolTimeNowCount = default;

    [SerializeField] GameObject m_shinzui = default; //スキル演出

    [SerializeField] int m_coolTime = 0; //再使用可能までのターン
    int m_timeCounter; //ターン計測用変数

    public bool m_canUse = false;

    bool m_flgStopper = false; 

    bool m_isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        m_shinzui.SetActive(false);
        m_skillPannel.SetActive(false);
        m_canUse = false;
    }

    void Update()
    {
        if(BattleManager._theTurn == BattleManager.Turn.InputTurn)
        {
            if(m_flgStopper == false)
            {
                m_timeCounter--;

                if(m_timeCounter == 0)
                {
                    m_canUse = true;
                    m_timeCounter = m_coolTime;
                }

                m_flgStopper = true;
            }
        }
        else if(BattleManager._theTurn == BattleManager.Turn.PlayerTurn)
        {
            m_flgStopper = false;
        }
    }

    public virtual void UseSkill()
    {
        if(m_canUse)
        {
            m_shinzui.SetActive(true);
        }
    }

    public virtual void Panneler(string name, string setumei)
    {
        if(m_isOpen == false)
        {
            m_skillName.text = name;
            m_skillSetumei.text = setumei;
            if(m_timeCounter > 0)
            {
                m_skillCoolTimeNowCount.text = "使用可能まで " + m_timeCounter.ToString() + " ターン";
            }
            else
            {
                m_skillCoolTimeNowCount.text = "使用後再使用まで " + m_coolTime.ToString() + " ターン";
            }
            m_skillPannel.SetActive(true);
        }
        else
        {
            m_skillPannel.SetActive(false);
        }
    }
}
