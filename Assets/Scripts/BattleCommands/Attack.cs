using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Attack : MonoBehaviour
{
    [SerializeField] Animator m_approchTobackWall = default; //接近アニメーション

    [SerializeField] GameObject m_syucyusen = default; // 集中線パーティクル。背景の接近アニメーションの終了時に自動で不可視化　＞＞BackWallBacker.cs

    [SerializeField] Target m_tergetIndexer = default; //ターゲット番号

    [SerializeField] Text m_diaLog = default;

    public float m_changeValueInterval = 1f; //値の変化速度

    private void Start()
    {
        m_syucyusen.SetActive(false);
    }

    public void AttackAct()
    {
        if(BattleManager._theTurn == BattleManager.Turn.InputTurn)
        {
            BattleManager._theTurn = BattleManager.Turn.PlayerTurn;

            m_approchTobackWall.SetBool("IsApproach", true);
            m_syucyusen.SetActive(true);

            m_diaLog.text = "～　<color=#8b0000>攻撃</color>　～　▽"; //赤字だぜ～

            float ite = EnemyStuts.m_enemiesStuts[m_tergetIndexer.m_tergetNum].Damage(Player.Instance.m_attack, false); //標的に対して通常攻撃

            //UI反映
            //Enemy.m_enemies[m_tergetIndexer.m_tergetNum].m_enemyHPSL.value = EnemyStuts.m_enemiesStuts[m_tergetIndexer.m_tergetNum].m_currentHP;


            DOTween.To(() => Enemy.m_enemies[m_tergetIndexer.m_tergetNum].m_enemyHPSL.value, x => Enemy.m_enemies[m_tergetIndexer.m_tergetNum].m_enemyHPSL.value = x,
                Enemy.m_enemies[m_tergetIndexer.m_tergetNum].m_enemyHPSL.value - ite, m_changeValueInterval);


            //残り回避率に応じて集中力を増加
            if (Player.Instance.m_currentDogePower > 0)
            {
                Player.Instance.m_currentConcentlate += (Player.Instance.m_currentDogePower / Player.Instance.m_dogePowerMax)*3;
            }

            //攻撃するたび回避率を減少
            if(Player.Instance.m_currentDogePower >= 5)
            {
                Player.Instance.m_currentDogePower -= 5;
            }
            else
            {
                Player.Instance.m_currentDogePower = 0;
            }
        }
        else
        {
            Debug.Log("今はインプットターンじゃないので攻撃できましぇん");
        }
    }
}
