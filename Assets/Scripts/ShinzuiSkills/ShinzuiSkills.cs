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

    Text m_dialog = default;

    public enum PannelingSkillKarsol
    {
        jiga,
        inga,
        syokeisya,
        shirogane,
        eva,
        _default,
    }
    static PannelingSkillKarsol nowPannelingSkillInfo = PannelingSkillKarsol._default; 

    // Start is called before the first frame update
    void Start()
    {
        m_dialog = GameObject.Find("DiaText").GetComponent<Text>();

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
        m_dialog.text = "";
        m_shinzuiEnsyutu.SetActive(true);
        m_skillPannel.SetActive(false);
        m_timeCounter = m_coolTime;
        m_canUse = false;
    }

    public virtual void Panneler(string name, string setumei, PannelingSkillKarsol karsol)
    {
        if( m_skillPannel.activeSelf == false || karsol != nowPannelingSkillInfo)
        {
            m_skillName.text = name;
            m_skillSetumei.text = setumei;
            if(m_timeCounter > 0)
            {
                m_skillCoolTimeNowCount.text = "残り " + m_timeCounter.ToString() + " ターン";
            }
            else
            {
                m_skillCoolTimeNowCount.text = "再使用 " + m_coolTime.ToString() + " ターン";
            }
            foreach(var x in m_children)
            {
                x.SetActive(false);
            }
            m_kaihouBottun.SetActive(true);
            m_skillPannel.SetActive(true);
            nowPannelingSkillInfo = karsol;
        }
        else
        {
            m_skillPannel.SetActive(false);
        }
    }
}
