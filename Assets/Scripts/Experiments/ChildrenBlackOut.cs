using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ChildrenBlackOut : MonoBehaviour
{
    List<SpriteRenderer> m_childrenSpr = new List<SpriteRenderer>();

    Animator a;
    Animator a2;

    void BlackOutCheck(Transform t, float animSpeed, int setLayer)
    {
        if(t.childCount > 0)
        {
            if (t.gameObject.GetComponent<Animator>())
            {
                if(a == null)
                {
                    a = t.gameObject.GetComponent<Animator>();
                }
                a.speed = animSpeed;
            }
            for (var i = 0; i < t.childCount; i++)
            {
                if (t.GetChild(i).gameObject.GetComponent<SpriteRenderer>())
                {
                    m_childrenSpr.Add(t.GetChild(i).gameObject.GetComponent<SpriteRenderer>());
                    t.GetChild(i).gameObject.layer = setLayer;

                }
                if (t.GetChild(i).gameObject.GetComponent<Animator>())
                {
                    if(a2 == null)
                    {
                        a2 = t.GetChild(i).gameObject.GetComponent<Animator>();
                    }
                    a2.speed = animSpeed;
                }
                BlackOutCheck(t.GetChild(i), animSpeed, setLayer);
            }
        }
    }

    public void Kamigakari()
    {
        BlackOutCheck(transform, 0, 13); //Upper(エフェクト外レイヤー)
        BlackOut(Color.black);
    }

    void BlackOut(Color c)
    {
        for(var i = 0; i < m_childrenSpr.Count; i++)
        {
            m_childrenSpr[i].color = c;
        }
    }

    public void TimeIsBack()
    {
        BlackOutCheck(transform, 1, 10); //エネミーレイヤー
        BlackOut(Color.white);
    }
}
