using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance = default;

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
        /// <summary> BattleEnd = 戦闘終了時。 </summary>
        BattleEnd,
    }
    public Turn theTurn = Turn.AwakeTurn;

    private void Awake()
    {
        theTurn = Turn.AwakeTurn;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 次のターンへ進める。
    /// </summary>
    public static void TurnAdvance()
    {
        BattleManager.Instance.theTurn++;
    }
}
