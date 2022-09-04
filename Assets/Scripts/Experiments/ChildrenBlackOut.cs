using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ChildrenBlackOut : MonoBehaviour
{
    List<SpriteRenderer> m_childrenSpr = new List<SpriteRenderer>();

    [SerializeField] GameObject[] m_gameObjects;

    [SerializeField] PostProcessVolume m_ppv;

    void BlackOutCheck(Transform t)
    {
        if(t.childCount > 0)
        {
            if (t.gameObject.GetComponent<Animator>())
            {
                Animator a = t.gameObject.GetComponent<Animator>();
                a.speed = 0;
            }
            for (var i = 0; i < t.childCount; i++)
            {
                if (t.GetChild(i).gameObject.GetComponent<SpriteRenderer>())
                {
                    m_childrenSpr.Add(t.GetChild(i).gameObject.GetComponent<SpriteRenderer>());
                    t.GetChild(i).gameObject.layer = 13;

                }
                if (t.GetChild(i).gameObject.GetComponent<Animator>())
                {
                    Animator a2 = t.GetChild(i).gameObject.GetComponent<Animator>();
                    a2.speed = 0;
                }
                BlackOutCheck(t.GetChild(i));
            }
        }
    }

    public void KamigakariTest()
    {
        BlackOutCheck(transform);

        for(var i = 0; i < m_gameObjects.Length; i++)
        {
            m_gameObjects[i].SetActive(true);
        }
        BlackOut();
        m_ppv.profile.GetSetting<ColorGrading>().contrast.value = -200f;
    }

    void BlackOut()
    {
        for(var i = 0; i < m_childrenSpr.Count; i++)
        {
            m_childrenSpr[i].color = Color.black;
        }
    }
}
