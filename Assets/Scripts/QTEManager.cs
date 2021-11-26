using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEManager : MonoBehaviour
{
    [SerializeField] GameObject[] m_QTEEffects = new GameObject[1]; //QTEのエフェクトを格納。

    [SerializeField] float m_waitTime = 3.0f; //何秒待つか。

    [SerializeField] GameObject m_pulusPos;
    [SerializeField] GameObject m_minusPos;

    float m_posXRand, m_posYRand;

    Vector2 m_effectPos; //生成位置

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(QTESys(m_waitTime+5));
    }

    IEnumerator QTESys(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        QTEGeneration();

    }

    void QTEGeneration()
    {
        if(BattleManager.theTurn == BattleManager.Turn.InputTurn)
        {
            m_posXRand = Random.Range(m_minusPos.transform.position.x, m_pulusPos.transform.position.x);
            m_posYRand = Random.Range(m_minusPos.transform.position.y, m_pulusPos.transform.position.y);
            m_effectPos = new Vector2(m_posXRand, m_posYRand);
            var x = Instantiate(m_QTEEffects[0]);
            x.transform.position = m_effectPos;
        }
        StartCoroutine(QTESys(m_waitTime));
    }
}
