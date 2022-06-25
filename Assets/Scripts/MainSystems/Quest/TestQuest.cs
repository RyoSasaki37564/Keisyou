using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestQuest : QuestBase
{
    GameObject m_player;

    private void Awake()
    {
        m_player = GameObject.FindGameObjectWithTag("PlayerAvater");
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
                    base.QuestEnd();
                }
                break;
        }
    }
}
