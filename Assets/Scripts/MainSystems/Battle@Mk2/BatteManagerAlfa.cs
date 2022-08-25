using System.Collections;
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
        StatusStartUP();
    }

    void StatusStartUP()
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
        m_playerConSlider.value = m_playerCon;
        SliderResize(m_playerConSlider, m_playerCon);

        m_playerDodgSlider.maxValue = m_playerDodg;
        m_playerDodgSlider.value = m_playerDodg;
    }

    void SliderResize(Slider slider, int stuts)
    {
        //パラメータに応じてゲージの長さ600まで変化。なお回避ゲージはこれを通さないこと
        RectTransform rect = slider.gameObject.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(stuts / 2 < 600 ? stuts - (stuts - 100) / 2 : 600, rect.sizeDelta.y);
        Debug.Log($"{rect.sizeDelta.x}, {stuts}");
        rect.localPosition = new Vector3(-300f + ((rect.sizeDelta.x - 100) / 2), rect.localPosition.y, rect.localPosition.z);
    }

    public void TestAttack()
    {

    }
}
