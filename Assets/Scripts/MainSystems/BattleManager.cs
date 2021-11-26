using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance = default;

    public Slider m_enemyHPSlider = default;

    [SerializeField] float m_enmyHPMax = 200;
    public float m_currentEnemyHP = 0;

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
        if (Instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = new BattleManager();
            DontDestroyOnLoad(this.gameObject);
        }

        theTurn = Turn.AwakeTurn;
        StartCoroutine(Instance.AwakeOff());
    }

    // Start is called before the first frame update
    void Start()
    {
        Instance.m_enemyHPSlider = GameObject.Find("EHPSlider").GetComponent<Slider>();
        Instance.m_enemyHPSlider.maxValue = m_enmyHPMax;
        m_currentEnemyHP = Instance.m_enemyHPSlider.maxValue;
        Instance.m_enemyHPSlider.value = m_currentEnemyHP;
        Debug.Log("やってまいりました。");
    }

    // Update is called once per frame
    void Update()
    {
        if(Instance.theTurn == Turn.PlayerTurn)
        {
            StartCoroutine(Instance.Turning());
        }
    }

    IEnumerator AwakeOff()
    {
        yield return new WaitForSeconds(3);
        Instance.theTurn = Turn.InputTurn;
    }
    IEnumerator Turning()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("インプットターンやで");
        Instance.theTurn = Turn.InputTurn;
    }

    /// <summary>
    /// 次のターンへ進める。
    /// </summary>
    public void TurnAdvance()
    {
        Instance.theTurn++;
        Debug.LogWarning(Instance.theTurn);
    }
}
