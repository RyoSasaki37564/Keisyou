using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tips : MonoBehaviour
{
    [SerializeField] List<GameObject> m_TipsMaster = new List<GameObject>();

    // Start is called before the first frame update
    public void Start()
    {
        Tipping(0);
    }

    public void TipsBottun_0()
    {
        Tipping(0);
    }
    public void TipsBottun_1()
    {
        Tipping(1);
    }
    public void TipsBottun_2()
    {
        Tipping(2);
    }
    public void TipsBottun_3()
    {
        Tipping(3);
    }
    public void TipsBottun_4()
    {
        Tipping(4);
    }
    public void TipsBottun_5()
    {
        Tipping(5);
    }
    public void TipsBottun_6()
    {
        Tipping(6);
    }
    public void TipsBottun_7()
    {
        Tipping(7);
    }
    public void TipsBottun_8()
    {
        Tipping(8);
    }

    void Tipping(int i)
    {
        foreach(var x in m_TipsMaster)
        {
            x.SetActive(false);
        }

        m_TipsMaster[i].SetActive(true);
    }
}
