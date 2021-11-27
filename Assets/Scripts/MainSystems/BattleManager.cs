using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    Slider m_enemyHPSlider = default;

    [SerializeField] float m_enmyHPMax = 200;
    public static float m_currentEnemyHP = 0;

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
    public static Turn theTurn = Turn.AwakeTurn;

    private void Awake()
    {
        theTurn = Turn.AwakeTurn;
        StartCoroutine(AwakeOff());
    }

    // Start is called before the first frame update
    void Start()
    {
        m_enemyHPSlider = GameObject.Find("EHPSlider").GetComponent<Slider>();
        m_enemyHPSlider.maxValue = m_enmyHPMax;
        m_currentEnemyHP = m_enemyHPSlider.maxValue;
        m_enemyHPSlider.value = m_currentEnemyHP;
        Debug.Log("やってまいりました。");
    }

    // Update is called once per frame
    void Update()
    {
        if(theTurn == Turn.PlayerTurn)
        {
            StartCoroutine(Turning());
        }

        m_enemyHPSlider.value = m_currentEnemyHP;
    }

    IEnumerator AwakeOff()
    {
        yield return new WaitForSeconds(3);
        theTurn = Turn.InputTurn;
    }
    IEnumerator Turning()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("インプットターンやで");
        theTurn = Turn.InputTurn;
    }

    /// <summary>
    /// 次のターンへ進める。
    /// </summary>
    public static void TurnAdvance()
    {
        theTurn++;
        Debug.LogWarning(theTurn);
    }
}
