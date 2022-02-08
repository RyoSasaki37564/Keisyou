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
    [SerializeField] GameObject m_kaihouBottun = default;
    [SerializeField] GameObject m_kaihouBottunParent = default;
    List<GameObject> m_children = new List<GameObject>();

    [SerializeField] GameObject m_shinzuiEnsyutu = default; //スキル演出

    [SerializeField] int m_coolTime = 0; //再使用可能までのターン
    int m_timeCounter; //ターン計測用変数

    public bool m_canUse = false;

    bool m_flgStopper = false; 

    bool m_isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        for(var i = 0; i < m_kaihouBottunParent.transform.childCount; i++)
        {
            m_children.Add(m_kaihouBottunParent.transform.GetChild(i).gameObject);
        }
        m_shinzuiEnsyutu.SetActive(false);
        m_skillPannel.SetActive(false);
        m_kaihouBottun.SetActive(false);
        m_canUse = true;//false;
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
        if(m_canUse == true)
        {
            Debug.Log("解放");
            m_shinzuiEnsyutu.SetActive(true);
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
            foreach(var x in m_children)
            {
                x.SetActive(false);
            }
            m_kaihouBottun.SetActive(true);
            m_skillPannel.SetActive(true);
            m_isOpen = true;
        }
        else
        {
            m_skillPannel.SetActive(false);
            m_isOpen = false;
        }
    }
}
