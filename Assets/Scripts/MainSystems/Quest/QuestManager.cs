using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] public QuestBase[] m_quests = new QuestBase[0];

    public static QuestManager Instance;

    private void Awake()
    {
        if(!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    //private void Update()
    //{
    //    if(m_quests.Length > 0)
    //    {
    //        int endCount = 0;
    //        foreach (var q in m_quests)
    //        {
    //            if(!q.m_endFlg)
    //            {
    //                q.QStateAdvance();
    //            }
    //            else
    //            {
    //                endCount++;
    //            }

    //            if(m_quests.Length <= 0)
    //            {
    //                return;
    //            }
    //        }

    //        if(endCount > 0)
    //        {
    //            var q = new QuestBase[m_quests.Length - endCount];
    //            int x = 0;
    //            for(var i = 0; i < m_quests.Length; i++)
    //            {
    //                if(!m_quests[i].enabled)
    //                {
    //                    q[x] = m_quests[i];
    //                    x++;
    //                }
    //                else
    //                {
    //                    Destroy(m_quests[i].gameObject);
    //                }
    //            }
    //            m_quests = q;
    //        }
    //    }
    //}
}
