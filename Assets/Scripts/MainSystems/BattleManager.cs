﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [SerializeField] Text m_diaLogText = default; //戦闘中のダイアログ

    /// <summary>
    /// 戦闘シーンにおけるシーン定義。
    /// </summary>
    public enum Turn
    {
        /// <summary> AwakeTurn = 戦闘開始時。原則Awake()以外では呼んではならない。 </summary>
        AwakeTurn,
        /// <summary> InputTurn = 入力待ち。 </summary>
        InputTurn,
        /// <summary> PlayerTurn = プレイヤー行動演出ターン。 </summary>
        PlayerTurn,
        /// <summary> EnemyTurn = 敵行動演出ターン。 </summary>
        EnemyTurn,
        /// <summary> TurnEnd = ターン終了時。 </summary>
        TurnEnd,
        /// <summary> BattleEnd = バトル終了時。 </summary>
        BattleEnd,
    }
    public static Turn _theTurn = Turn.AwakeTurn;

    bool m_diaLogStopper = false;

    private void Awake()
    {
        _theTurn = Turn.AwakeTurn;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("やってまいりました。");
    }

    // Update is called once per frame
    void Update()
    {

        switch (_theTurn)
        {
            case Turn.AwakeTurn:
                m_diaLogText.text = "狩りだ　▽";
                if(Input.GetButton("Fire1"))
                {
                    _theTurn = Turn.InputTurn;
                }
                break;

            case Turn.InputTurn:
                m_diaLogText.text = "どうする？　▽";
                break;

            case Turn.PlayerTurn:
                break;

            case Turn.EnemyTurn:
                if(m_diaLogStopper == false)
                {
                    m_diaLogText.text = "敵の行動　▽";
                    m_diaLogStopper = true;
                }
                break;

            case Turn.TurnEnd:
                m_diaLogStopper = false;
                if(Player.Instance.m_currentHP <= 0)
                {
                    _theTurn = Turn.BattleEnd;
                }
                else
                {
                    _theTurn = Turn.InputTurn;
                }
                break;

            case Turn.BattleEnd:
                m_diaLogText.text = " <color=#8b0000>死</color>　▽";
                break;
        }
    }

    /// <summary> 次のターンへ進める。</summary>
    public static IEnumerator TurnAdvance()
    {
        Debug.LogWarning(_theTurn);
        yield return new WaitForSeconds(1.5f);
        _theTurn++;
    }
}
