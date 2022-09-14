using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSignalInner : MonoBehaviour
{
    [SerializeField] BatteManagerAlfa m_bma;

    //攻撃に応じて、そのスパンにあった体力減少演出時間を入れる
    [SerializeField] List<float> m_doTimeList = new List<float>();

    int m_count = 0;

    private void OnEnable()
    {
        m_count = 0;
    }

    public void NomalAttackFromSignal()
    {
        m_bma.NomalAttackDamage(m_doTimeList[m_count], m_count + 1 == m_doTimeList.Count ? true : false); //最終だったらtrue
        if(m_doTimeList.Count > 1)
        {
            m_count++;
        }
    }
}
