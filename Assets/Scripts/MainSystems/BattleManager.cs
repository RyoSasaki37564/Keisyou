using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [SerializeField] Text m_diaLogText = default; //戦闘中のダイアログ

    [SerializeField] List<SpriteRenderer> m_ryuunoko = new List<SpriteRenderer>();
    float m_offPoint = 0.01f;
    [SerializeField] float m_push = 0.01f;

    [SerializeField] GameObject m_MAKUHIKI = default;

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

    [SerializeField] GameObject m_UICanvas = default;

    bool m_goToTitle = false;

    private void Awake()
    {
        _theTurn = Turn.AwakeTurn;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("やってまいりました。");
        TouchManager.Began += (info) =>
        {
            if(_theTurn == Turn.AwakeTurn)
            {
                _theTurn = Turn.InputTurn;
                m_diaLogText.text = "どうする？　▽";
            }
        };


        m_UICanvas.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        switch (_theTurn)
        {
            case Turn.AwakeTurn:
                m_goToTitle = false;
                m_diaLogText.text = "狩りだ　▽";
                break;

            case Turn.InputTurn:
                if (Player.Instance.DeadOrAlive() == true ||
                    EnemyStuts.m_enemiesStuts[Target.m_tergetNum].DeadOrAlive() == true)
                {
                    _theTurn = Turn.BattleEnd;
                }
                break;

            case Turn.PlayerTurn:
                if (Player.Instance.DeadOrAlive() == true ||
                    EnemyStuts.m_enemiesStuts[Target.m_tergetNum].DeadOrAlive() == true)
                {
                    _theTurn = Turn.BattleEnd;
                }
                break;

            case Turn.EnemyTurn:
                if (Player.Instance.DeadOrAlive() == true ||
                    EnemyStuts.m_enemiesStuts[Target.m_tergetNum].DeadOrAlive() == true)
                {
                    _theTurn = Turn.BattleEnd;
                }
                break;

            case Turn.TurnEnd:
                if(Player.Instance.DeadOrAlive() == true ||
                    EnemyStuts.m_enemiesStuts[Target.m_tergetNum].DeadOrAlive() == true)
                {
                    _theTurn = Turn.BattleEnd;
                }
                else
                {
                    _theTurn = Turn.InputTurn;
                    m_diaLogText.text = "どうする？　▽";
                }
                break;

            case Turn.BattleEnd:
                if (Player.Instance.DeadOrAlive() == true)
                {
                    m_diaLogText.text = " <color=#8b0000>死</color>　▽";
                }
                else if(EnemyStuts.m_enemiesStuts[Target.m_tergetNum].DeadOrAlive() == true)
                {
                    m_diaLogText.text = " <color=#8b0000>狩りは完遂した</color>　▽";
                    if (m_offPoint <= 254)
                    {
                        AlfaDown();
                    }
                    if(m_goToTitle == false)
                    {
                        m_MAKUHIKI.SetActive(true);
                        m_goToTitle = true;
                    }
                }
                break;
        }
    }

    void AlfaDown()
    {
        foreach (var i in m_ryuunoko)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - m_offPoint);
        }
        m_offPoint += m_push;

    }
}
