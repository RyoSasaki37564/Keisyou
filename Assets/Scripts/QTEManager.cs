﻿using System.Collections;
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

    public bool m_isQTETime;

    [SerializeField] Animator m_anim = default;

    [SerializeField] Stop m_pr = default;

    void OnEnable()
    {
        m_pr.OnPauseResume += PauseResume;
    }
    void OnDisable()
    {
        m_pr.OnPauseResume -= PauseResume;
    }

    void Start()
    {
        m_isQTETime = true;
        StartCoroutine(QTESys(m_waitTime));
    }

    IEnumerator QTESys(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        QTEGeneration();

    }

    void PauseResume(bool isPause)
    {
        if (isPause)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }
    public void Pause()
    {
        m_isQTETime = false;
    }
    public void Resume()
    {
        m_isQTETime = true;
    }

    void QTEGeneration()
    {
        if(EnemyStuts.m_enemiesStuts[Target.m_tergetNum].DeadOrAlive() == false &&
            Player.Instance.DeadOrAlive() == false)
        {
            var r = Random.Range(1, 3);

            if (r == 1 && m_isQTETime == true &&
                BattleManager._theTurn == BattleManager.Turn.InputTurn ||
                BattleManager._theTurn == BattleManager.Turn.EnemyTurn &&
                Ryugeki.m_isHitRyugeki == false)
            {
                m_posXRand = Random.Range(m_minusPos.transform.position.x, m_pulusPos.transform.position.x);
                m_posYRand = Random.Range(m_minusPos.transform.position.y, m_pulusPos.transform.position.y);
                m_effectPos = new Vector2(m_posXRand, m_posYRand);

                int rand;

                if (EnemyStuts.m_enemiesStuts[Target.m_tergetNum].m_currentHP > EnemyStuts.m_enemiesStuts[Target.m_tergetNum].m_maxHP / 2)
                {
                    rand = Random.Range(0, (int)(m_QTEEffects.Length * 0.7f));
                }
                else
                {
                    rand = Random.Range(m_QTEEffects.Length / 2, m_QTEEffects.Length);
                }

                var x = Instantiate(m_QTEEffects[rand]);

                m_anim.SetInteger("AttackMotion1", 1);

                x.transform.position = m_effectPos;
                StartCoroutine(QTESys(m_waitTime));
            }
            else
            {
                StartCoroutine(QTESys(m_waitTime));
            }
        }
    }
}