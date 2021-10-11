using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEManager : MonoBehaviour
{
    [SerializeField] GameObject[] m_QTEEffects = new GameObject[1]; //QTEのエフェクトを格納。

    [SerializeField] float m_waitTime = 3.0f; //何秒待つか。


    float m_posX, m_posY;
    Transform m_pos; //生成位置

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(QTESys(m_waitTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator QTESys(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        QTEGeneration();

    }

    void QTEGeneration()
    {
        m_posX = Random.Range(-200f, 200f);
        m_posY = Random.Range(-150f, 150f);
        m_pos = new Transform()
        Instantiate(m_QTEEffects[0], m_pos.transform.position, m_pos.transform.rotation);
        StartCoroutine(QTESys(m_waitTime));
    }
}
