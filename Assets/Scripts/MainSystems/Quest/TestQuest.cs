using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestQuest : QuestBase
{
    TestQuest m_instanse = new TestQuest("テスト", "テストの説明", 3);

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
                m_setumei = "クエストスタート";
                m_counter++;
                break;
            case 1:
                break;
            case 2:
                m_setumei = "クエスト完了";
                break;
        }
    }
}
