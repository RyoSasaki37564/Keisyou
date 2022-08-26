﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PhaseOnBattle
{
    Input,
    Player,
    Enemy,
}

public class BatteManagerAlfa : MonoBehaviour
{
    [SerializeField] Slider m_playerHPSlider;
    [SerializeField] Slider m_playerConSlider;
    [SerializeField] Slider m_playerDodgSlider;

    [SerializeField] GameObject m_battleUICanvas;

    [SerializeField] GameObject m_tempEnemyHPSlider;
    List<EnemyStatusAlfa> m_enemyInstanceList = new List<EnemyStatusAlfa>();
    List<Slider> m_enemyHPBarList = new List<Slider>();
    public List<int> m_enemyEncountIDList = new List<int>();

    int m_playerHP;
    public int GetPHP { get => m_playerHP; }
    int m_playerCon;
    public int GetPCon { get => m_playerCon; }
    int m_playerDodg;
    public int GetDodg { get => m_playerDodg; }
    int m_playerAtk;
    public int GetPAtk { get => m_playerAtk; }
    int m_playerDef;
    public int GetPDef { get => m_playerDef; }
    int m_playerKakugo;
    public int GetKakugo { get => m_playerKakugo; }

    // Start is called before the first frame update
    void Start()
    {
        //テストエンカウント
        m_enemyEncountIDList.Add(0);
        m_enemyEncountIDList.Add(5);
        m_enemyEncountIDList.Add(6);

        PlayerStatusSetUP();
        EnemyStatusSetUP(m_enemyEncountIDList);
    }

    void EnemyStatusSetUP(List<int> idList)
    {
        for(var i = 0; i < idList.Count; i++)
        {
            EnemyStatusAlfa ene = PlayerDataAlfa.Instance.m_enemyTable[idList[i]];
            m_enemyInstanceList.Add(ene);
            GameObject eneSliObj = Instantiate(m_tempEnemyHPSlider, m_battleUICanvas.transform);
            eneSliObj.SetActive(true);
            Slider eneHPSli = eneSliObj.GetComponent<Slider>();
            eneHPSli.maxValue = ene.m_hp;
            eneHPSli.value = eneHPSli.maxValue;
            RectTransform rect = eneSliObj.GetComponent<RectTransform>();
            rect.localPosition = new Vector3(rect.localPosition.x, rect.localPosition.y + (35 * i), rect.localPosition.z);
            Text eneName = eneSliObj.transform.Find("Name").GetComponent<Text>();
            eneName.text = ene.m_name;
        }
    }

    void PlayerStatusSetUP()
    {
        m_playerHP = PlayerDataAlfa.Instance.GetStuts.m_hp;
        m_playerCon = PlayerDataAlfa.Instance.GetStuts.m_con;
        m_playerDodg = PlayerDataAlfa.Instance.GetStuts.m_dodge;
        m_playerAtk = PlayerDataAlfa.Instance.GetStuts.m_atk;
        m_playerDef = PlayerDataAlfa.Instance.GetStuts.m_def;
        m_playerKakugo = PlayerDataAlfa.Instance.GetStuts.m_kakugo;

        m_playerHPSlider.maxValue = m_playerHP;
        m_playerHPSlider.value = m_playerHP;
        SliderResize(m_playerHPSlider, m_playerHP);

        m_playerConSlider.maxValue = m_playerCon;
        m_playerConSlider.value = 0;
        SliderResize(m_playerConSlider, m_playerCon);

        m_playerDodgSlider.maxValue = m_playerDodg;
        m_playerDodgSlider.value = m_playerDodg;
    }

    void SliderResize(Slider slider, int stuts)
    {
        //パラメータに応じてゲージの長さ600まで変化。なお回避ゲージはこれを通さないこと
        RectTransform rect = slider.gameObject.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(stuts / 2 < 600 ? stuts - (stuts - 100) / 2 : 600, rect.sizeDelta.y);
        rect.localPosition = new Vector3(-300f + ((rect.sizeDelta.x - 100) / 2), rect.localPosition.y, rect.localPosition.z);
    }

    public void TestAttack()
    {

    }
}
