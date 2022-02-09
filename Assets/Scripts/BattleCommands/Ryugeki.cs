using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ryugeki : MonoBehaviour
{
    [SerializeField] GameObject m_nines = default; //九字鍵盤

    [SerializeField] Text m_dialog = default; //ダイアログ

    [SerializeField] List<GameObject> m_playerCommandsUI = new List<GameObject>(); //攻撃や回避などの龍撃以外のボタン系UIの親オブジェクト

    public static bool m_isHitRyugeki = false; //龍撃を行っているか否か

    [SerializeField] NineKeyInput m_nineKeysScript = default;

    [SerializeField] Image m_fader = default; //フェードインアウト用スクリーン

    [SerializeField] GameObject m_syucyusen = default; // 集中線パーティクル

    [SerializeField] GameObject m_ryugekiBottun = default; //このボタン

    bool m_flg = false;

    bool m_isRyugekiNyuryoku = false;

    float m_time = 0;

    public void RyugekiNoKamae()
    {

        if(m_isHitRyugeki == true)
        {
            m_syucyusen.SetActive(false);
            m_flg = true;
            StartCoroutine(FadeIn());
            m_nineKeysScript.Phaser();
            m_nines.SetActive(false);

            m_isHitRyugeki = false;
            m_isRyugekiNyuryoku = false;
        }
        else
        {
            if(Enemy.m_isRyugekiChance == true)
            {
                Time.timeScale = 1f;
                m_syucyusen.SetActive(true);
                StartCoroutine(FadeIn());
                BattleManager._theTurn = BattleManager.Turn.PlayerTurn;
                m_isHitRyugeki = true;
                foreach (var i in m_playerCommandsUI)
                {
                    i.SetActive(false);
                }
                m_nines.SetActive(true);
                m_dialog.text = "";
                m_isRyugekiNyuryoku = true;
            }
        }

    }

    private void Update()
    {
        if(m_isRyugekiNyuryoku == true && Player.Instance.m_currentConcentlate >= 0)
        {
            m_time += Time.deltaTime;
            if(m_time > 1)
            {
                Player.Instance.m_currentConcentlate -= 1;
                if (Player.Instance.m_currentConcentlate <= 0)
                {
                    Player.Instance.m_currentConcentlate = 0;
                }
                m_time = 0;
            }
        }
    }

    IEnumerator FadeIn()
    {
        m_fader.color = new Color(250, 250, 250, 250);
        yield return new WaitForSeconds(0.15f);
        m_fader.color = new Color(0, 0, 0, 0);
        BattleManager._theTurn = BattleManager.Turn.InputTurn;
        if(m_flg == true)
        {
            m_ryugekiBottun.SetActive(false);
            m_flg = false;
        }
    }
}
