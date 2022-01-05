using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [SerializeField] Text m_diaLogText = default; //戦闘中のダイアログ

    public static List<Enemy> m_enemies; //戦闘ごとに設定された数の敵実体を格納

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
    public static Turn _theTurn = Turn.AwakeTurn;

    private void Awake()
    {
        _theTurn = Turn.AwakeTurn;
        StartCoroutine(AwakeOff());
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("やってまいりました。");
    }

    // Update is called once per frame
    void Update()
    {
        if(_theTurn == Turn.PlayerTurn)
        {
            StartCoroutine(Turning());
        }

        switch (_theTurn)
        {
            case Turn.AwakeTurn:
                m_diaLogText.text = "";
                break;

            case Turn.InputTurn:
                m_diaLogText.text = "どうする？";
                break;

            case Turn.PlayerTurn:
                m_diaLogText.text = "";
                break;

            case Turn.EnemyTurn:
                m_diaLogText.text = "";
                break;

            case Turn.BattleEnd:
                m_diaLogText.text = "";
                break;
        }
    }

    IEnumerator AwakeOff()
    {
        yield return new WaitForSeconds(3);
        _theTurn = Turn.InputTurn;
    }
    IEnumerator Turning()
    {
        yield return new WaitForSeconds(0.1f);
        _theTurn = Turn.InputTurn;
    }

    /// <summary> 次のターンへ進める。</summary>
    public static void TurnAdvance()
    {
        _theTurn++;
    }
}
