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

    public void RyugekiNoKamae()
    {
        if(m_isHitRyugeki == true)
        {
            m_fader.color = new Color(250, 250, 250, 250);
            m_nineKeysScript.Phaser();
            Debug.Log("龍撃撃った " + BattleManager._theTurn);
            m_isHitRyugeki = false;
            foreach (var i in m_playerCommandsUI)
            {
                i.SetActive(true);
            }
            m_nines.SetActive(false);
            StartCoroutine(FadeIn());
        }
        else
        {
            if (BattleManager._theTurn == BattleManager.Turn.InputTurn ||
               Enemy.m_isRyugekiChance == true)
            {
                BattleManager._theTurn = BattleManager.Turn.PlayerTurn;
                m_isHitRyugeki = true;
                foreach (var i in m_playerCommandsUI)
                {
                    i.SetActive(false);
                }
                m_nines.SetActive(true);
                m_dialog.text = "";
            }
        }

    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(0.3f);
        m_fader.color = new Color(0, 0, 0, 0);
        BattleManager._theTurn = BattleManager.Turn.InputTurn;
    }
}
