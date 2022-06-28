using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldEventManager
{
    static FieldEventManager m_instance;
    public static FieldEventManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new FieldEventManager();
            }
            return m_instance;
        }
    }

    public FieldEventUnit m_nowSetEvent;

    public void TalkEvent()
    {
        if(m_nowSetEvent)
        {
            m_nowSetEvent.Talk();
        }
        else
        {
            Debug.LogError("イベントが存在しないんだが？");
        }
    }
}
