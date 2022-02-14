using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Doge : MonoBehaviour
{
    [SerializeField] Text m_diaLog = default;

    [SerializeField] Animator m_LeaveTobackWall = default; //飛び退りアニメーション


    public void DogeAct()
    {
        if (BattleManager._theTurn == BattleManager.Turn.InputTurn)
        {
            m_LeaveTobackWall.SetBool("IsDoge", true);
            BattleManager._theTurn = BattleManager.Turn.InputTurn;
            StartCoroutine(TurnSkip());
        }
    }

    IEnumerator TurnSkip()
    {
        m_diaLog.text = "～　<color=#00ced1>回避</color>　～　▽";

        Player.Instance.m_currentDogePower += Player.Instance.m_dogePowerMax / 10;

        //残り回避率に応じて集中力を増加
        if (Player.Instance.m_currentDogePower > 0)
        {
            Player.Instance.m_currentConcentlate += (int)(Player.Instance.m_currentDogePower / Player.Instance.m_dogePowerMax);
        }

        yield return new WaitForSeconds(1f);

        BattleManager._theTurn = BattleManager.Turn.TurnEnd;
    }
}
