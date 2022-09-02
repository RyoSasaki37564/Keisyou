using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PhaseOnBattle
{
    Start,
    Input,
    Player,
    Enemy,
    Kamigakari,
    End,
}

enum ModeOfZone
{
    None,
    Kamigakari,
    Homura,
    Meikyoushisui,
    Deep,
}

public class BatteManagerAlfa : MonoBehaviour
{
    PhaseOnBattle m_nowPhase = PhaseOnBattle.Start;
    ModeOfZone m_zone = ModeOfZone.None;

    [SerializeField] Text m_dialog;

    [SerializeField] Slider m_playerHPSlider;
    [SerializeField] Slider m_playerConSlider;
    [SerializeField] Slider m_playerDodgSlider;

    [SerializeField] GameObject m_battleUICanvas;
    [SerializeField] GameObject m_resultUICanvas;

    [SerializeField] GameObject m_tempEnemyHPSlider;
    List<EnemyStatusAlfa> m_enemyInstanceList = new List<EnemyStatusAlfa>();
    List<Slider> m_enemyHPBarList = new List<Slider>();
    List<EnemyBattleAIBase> m_enemyAIList = new List<EnemyBattleAIBase>();
    List<GameObject> m_enemyTargettingMarkerList = new List<GameObject>();
    public List<int> m_enemyEncountIDList = new List<int>();

    int m_target;
    public int GetTarget { get => m_target; }
    public int SetTarget { set => m_target = value; }

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

        PlayerStatusSetUP();
        EnemyStatusSetUP(m_enemyEncountIDList);
        StateProgression();
    }


    //void Update()
    //{
    //    StateProgression();
    //}

    void EnemyStatusSetUP(List<int> idList)
    {
        m_target = idList.Count - 1;

        for (var i = 0; i < idList.Count; i++)
        {
            EnemyStatusAlfa ene = PlayerDataAlfa.Instance.m_enemyTable[idList[i]];
            m_enemyInstanceList.Add(ene);
            GameObject eneSliObj = Instantiate(m_tempEnemyHPSlider, m_battleUICanvas.transform);
            eneSliObj.SetActive(true);
            GameObject targetMarker = eneSliObj.transform.Find("Targetting").gameObject;
            m_enemyTargettingMarkerList.Add(targetMarker);
            if(i == m_target)
            {
                targetMarker.SetActive(true);
            }
            Slider eneHPSli = eneSliObj.GetComponent<Slider>();
            m_enemyHPBarList.Add(eneHPSli);
            eneHPSli.maxValue = ene.m_hp;
            eneHPSli.value = eneHPSli.maxValue;
            RectTransform rect = eneSliObj.GetComponent<RectTransform>();
            rect.localPosition = new Vector3(rect.localPosition.x, rect.localPosition.y + (35 * i), rect.localPosition.z);
            Text eneName = eneSliObj.transform.Find("Name").GetComponent<Text>();
            eneName.text = ene.m_name;
            var eAI = EnemyAIGenerate(idList[i]);
            m_enemyAIList.Add(eAI);
        }
    }

    EnemyBattleAIBase EnemyAIGenerate(int id)
    {

        if(id < 5)
        {
            return new BattleAIDragon0();
        }
        else
        {
            return new BattleAIDragon0();
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

    public void TestDodge()
    {
        if (m_nowPhase == PhaseOnBattle.Input)
        {
            int testDodge = 10;
            float doTime = 1f;
            IEnumerator Progression()
            {
                yield return new WaitForSeconds(doTime);
                m_nowPhase = PhaseOnBattle.Enemy;
                StateProgression();
            }
            StartCoroutine(Progression());
            DOTween.To(() => m_playerDodgSlider.value, x => m_playerDodgSlider.value = x,
                    m_playerDodgSlider.value + testDodge, doTime);
        }
    }

    public void TestAttack()
    {
        if(m_nowPhase == PhaseOnBattle.Input)
        {
            Debug.LogError("attack");
            int damage = m_playerAtk - m_enemyInstanceList[m_target].m_def/10;
            if(damage < 0)
            {
                damage = 1;
            }
            int lessDodg = 10;
            float getConRate = 0.12f;

            float doTime = 1f;
            IEnumerator DeadCheck()
            {
                yield return new WaitForSeconds(doTime);
                IsEnemyDead();
            }
            StartCoroutine(DeadCheck());
            DOTween.To(() => m_enemyHPBarList[m_target].value, x => m_enemyHPBarList[m_target].value = x,
                    m_enemyHPBarList[m_target].value - damage, doTime);

            DOTween.To(() => m_playerDodgSlider.value, x => m_playerDodgSlider.value = x,
                    m_playerDodgSlider.value - lessDodg, doTime);

            DOTween.To(() => m_playerConSlider.value, x => m_playerConSlider.value = x,
                    m_playerConSlider.value + m_playerConSlider.maxValue * getConRate, doTime);
        }
    }

    void IsEnemyDead()
    {
        if(m_enemyHPBarList[m_target].value == 0)
        {
            for (var i = m_enemyHPBarList.Count - 1; i >= 0 ; i--)
            {
                if (m_enemyHPBarList[i].value != 0)
                {
                    int pTarget = m_target;
                    m_target = i;
                    ChangeTarget(pTarget, m_target);
                    m_nowPhase = PhaseOnBattle.Enemy;
                    StateProgression();
                    return;
                }
            }
            BattleEnd();
        }
        else
        {
            m_nowPhase = PhaseOnBattle.Enemy;
            StateProgression();
        }
    }

    void ChangeTarget(int prev, int next)
    {
        m_enemyTargettingMarkerList[prev].SetActive(false);
        m_enemyTargettingMarkerList[next].SetActive(true);
    }

    void BattleEnd()
    {
        m_battleUICanvas.SetActive(false);

        StartCoroutine(ResultOpen()); //実際には敵の死亡演出後。つまりここには本来敵の死亡演出呼び出しが入り、その演出からリザルトを起こす。
    }

    IEnumerator ResultOpen()
    {
        yield return new WaitForSeconds(1.5f);
        m_resultUICanvas.SetActive(true);
    }

    void StateProgression()
    {
        switch(m_nowPhase)
        {
            case PhaseOnBattle.Start:
                m_nowPhase = PhaseOnBattle.Input;
                break;
            case PhaseOnBattle.Input:
                m_dialog.text = "プレイヤーのターン";
                break;
            case PhaseOnBattle.Enemy:
                m_dialog.text = "敵の行動";
                EnemysSelectActions();
                break;
            case PhaseOnBattle.Kamigakari:
                break;
            case PhaseOnBattle.End:
                break;
        }
    }

    void EnemysSelectActions()
    {
        int i = m_enemyHPBarList.Count - 1;
        float doTime = 1.5f;

        StartCoroutine(DeadCheck());

        IEnumerator DeadCheck()
        {
            yield return new WaitForSeconds(doTime);
            if (m_enemyHPBarList[i].value != 0)
            {
                Debug.Log(m_enemyInstanceList[i].m_name + ", スタミナ" + m_enemyInstanceList[i].m_nowStamina);
                m_enemyAIList[i].EnemyActionSelect((int)m_enemyHPBarList[i].value, (int)m_enemyHPBarList[i].maxValue,
                    ref m_enemyInstanceList[i].m_nowStamina, m_enemyInstanceList[i].m_stamina, (int)m_playerDodgSlider.value, (int)m_playerDodgSlider.maxValue);
            }

            i--;

            if(i == -1)
            {
                m_nowPhase = PhaseOnBattle.Input;
                StateProgression();
            }
            else
            {
                StartCoroutine(DeadCheck());
            }
        }
    }

    void Zone()
    {
        switch (m_zone)
        {
            case ModeOfZone.None:
                Debug.LogWarning("極致になってねーのにZone()呼ばれたぞ");
                break;
            case ModeOfZone.Kamigakari:
                break;
            case ModeOfZone.Homura:
                break;
            case ModeOfZone.Meikyoushisui:
                break;
            case ModeOfZone.Deep:
                break;
        }
    }
}
