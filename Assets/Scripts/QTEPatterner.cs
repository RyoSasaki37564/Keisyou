using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEPatterner : MonoBehaviour
{
    [SerializeField] List<GameObject> m_QTEs = new List<GameObject>();

    int m_indexer = 0;

    [SerializeField] float m_interval = 1f;

    // Start is called before the first frame update
    void Start()
    {
        foreach(var i in m_QTEs)
        {
            i.SetActive(false);
        }
        QTEActivate();
    }

    void QTEActivate()
    {
        if(m_indexer != m_QTEs.Count)
        {
            StartCoroutine(QTERoader());
        }
    }

    IEnumerator QTERoader()
    {
        yield return new WaitForSeconds(m_interval);
        m_QTEs[m_indexer].SetActive(true);
        m_indexer++;
    }
}
