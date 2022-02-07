using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineEffectMnager : MonoBehaviour
{
    [SerializeField] GameObject m_speedLine = default; //トレイルがついてるオブジェクト。線書くぜ

    [SerializeField] int m_lineCount = 100;

    int m_indexer = 0;

    [SerializeField] GameObject m_maxHeight;
    [SerializeField] GameObject m_minHeight;

    [SerializeField] float m_interval = 0.05f;

    float rand;

    [SerializeField] public bool m_isTate = false;

    private void OnEnable()
    {
        // m_lineCount の数だけ線をプールする
        for (var i = 0; i < m_lineCount; i++)
        {
            var x = Instantiate(m_speedLine);
            x.transform.SetParent(this.gameObject.transform);
            x.SetActive(false);
        }
        m_indexer = 0;
        Liner();
    }

    void Liner()
    {
        if(m_isTate == false)
        {
            rand = Random.Range(m_minHeight.transform.position.y, m_maxHeight.transform.position.y);
            StartCoroutine(SpeedLiner());
        }
        else
        {
            rand = Random.Range(m_minHeight.transform.position.x, m_maxHeight.transform.position.x);
            StartCoroutine(UpperLiner());
        }
    }

    IEnumerator SpeedLiner()
    {
        yield return new WaitForSeconds(m_interval);
        var x = this.gameObject.transform.GetChild(m_indexer).gameObject;
        x.transform.position = new Vector3(this.transform.position.x, rand, this.transform.position.z);
        x.SetActive(true);
        m_indexer++;
        if (m_indexer == m_lineCount)
        {
            m_indexer = 0;
        }
        Liner();
    }
    IEnumerator UpperLiner()
    {
        yield return new WaitForSeconds(m_interval);
        var x = this.gameObject.transform.GetChild(m_indexer).gameObject;
        x.transform.position = new Vector3(rand, this.transform.position.y, this.transform.position.z);
        x.SetActive(true);
        m_indexer++;
        if (m_indexer == m_lineCount)
        {
            m_indexer = 0;
        }
        Liner();
    }
}
