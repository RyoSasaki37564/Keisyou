using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNineKeyOpen : MonoBehaviour
{
    [SerializeField] List<GameObject> m_goList = new List<GameObject>();

    public List<Collider2D> m_enemyBodyColList = new List<Collider2D>();

    bool m_test;

    private void Start()
    {
        foreach (var g in m_goList)
        {
            if(g.activeSelf)
            {
                g.SetActive(false);
            }
        }
    }

    public void Test()
    {
        if(!m_test)
        {
            foreach(var g in m_goList)
            {
                g.SetActive(true);
;           }
            foreach(var c in m_enemyBodyColList)
            {
                c.enabled = false;
            }
            m_test = true;
        }
        else
        {
            foreach (var g in m_goList)
            {
                g.SetActive(false);
            }
            foreach (var c in m_enemyBodyColList)
            {
                c.enabled = true;
            }
            m_goList[2].SetActive(true);
            m_test = false;
        }
    }
}
