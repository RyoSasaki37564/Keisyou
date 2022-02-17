using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEPatterner : MonoBehaviour
{
    public List<GameObject> m_QTEs = new List<GameObject>(); //発生させたいQTE

    public int m_indexer = 0;

    [SerializeField] float m_interval = 1f; //何秒で次を出すか

    private void Start()
    {
        foreach (var i in m_QTEs)
        {
            i.SetActive(false);
        }
        QTEActivate();
    }

    public void QTEActivate()
    {
        //出し切ってなけりゃ続行
        if(m_indexer != m_QTEs.Count)
        {
            StartCoroutine(QTERoader());
        }
    }

    public virtual IEnumerator QTERoader()
    {
        //発生装置本体。QTEの道ができるから「Roader」。スペルミスちゃうで。
        yield return new WaitForSeconds(m_interval);
        m_QTEs[m_indexer].SetActive(true);
        m_indexer++;
        QTEActivate();
    }
}
