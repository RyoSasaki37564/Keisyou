using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestQuest : QuestBase
{
    GameObject m_player;

    //TestAbility0 m_testAbility0;

    private void OnEnable()
    {
        SetUp();
    }

    private void Awake()
    {
        SetUp();
    }

    void SetUp()
    {
        if (m_player == null)
        {
            m_player = GameObject.FindGameObjectWithTag("PlayerAvater");
        }
        /*
        if (m_testAbility0 == null)
        {
            m_testAbility0 = FindObjectOfType<TestAbility0>().GetComponent<TestAbility0>();
        }
        */
    }

    public TestQuest(string name, string setumei, int flgCount) : base(name, setumei, flgCount)
    {
        m_name = name;
        m_setumei = setumei;
        m_shinkoudoFlgs = new bool[flgCount];
    }

    public override void QStateAdvance()
    {
        switch(m_counter)
        {
            case 0:
                if(m_player.transform.position == new Vector3(28, -2, m_player.transform.position.z))
                {
                    m_counter++;
                    m_setumei = "クエストスタート";
                }
                break;
            case 1:
                if (m_player.transform.position == new Vector3(28, -5, m_player.transform.position.z))
                {
                    m_counter++;
                    m_setumei = "クエストクリア";
                }
                break;
            case 2:
                if (m_player.transform.position == new Vector3(28, -4, m_player.transform.position.z))
                {
                    /*
                    m_testAbility0.CanActivateTrue();
                    */
                    base.QuestEnd();
                }
                break;
        }
    }
}
