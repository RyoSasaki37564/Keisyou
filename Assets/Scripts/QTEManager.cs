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

    [SerializeField] bool m_isQTETime = false;

    [SerializeField] Animator m_anim = default;

    void Start()
    {
        StartCoroutine(QTESys(m_waitTime));
    }

    IEnumerator QTESys(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        QTEGeneration();

    }

    void QTEGeneration()
    {
        var r = Random.Range(1, 3);
        if(r == 1)
        {
            m_isQTETime = true;
        }

        if(m_isQTETime == true && BattleManager._theTurn == BattleManager.Turn.InputTurn)
        {
            m_posXRand = Random.Range(m_minusPos.transform.position.x, m_pulusPos.transform.position.x);
            m_posYRand = Random.Range(m_minusPos.transform.position.y, m_pulusPos.transform.position.y);
            m_effectPos = new Vector2(m_posXRand, m_posYRand);
            int rand = Random.Range(0, m_QTEEffects.Length);

            var x = Instantiate(m_QTEEffects[rand]);

            m_anim.SetInteger("AttackMotion1", 1);

            x.transform.position = m_effectPos;
            m_isQTETime = false;
            StartCoroutine(QTESys(m_waitTime));
        }
        else
        {
            StartCoroutine(QTESys(m_waitTime));
        }
    }
}