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
    bool m_nowZone = false;


    //シェイク関連のパラメータ
    [SerializeField] Transform m_cameraTF;

    [SerializeField] Text m_dialog;

    [SerializeField] Slider m_playerHPSlider;
    [SerializeField] Slider m_playerConSlider;
    [SerializeField] Slider m_playerDodgSlider;

    
    [SerializeField] Transform m_shinzuiCircle;
    [SerializeField] Transform m_shinzuiPool;
    [SerializeField] GameObject[] m_shinzuiButtons = new GameObject[5];
    [SerializeField] ArmsSysAlfa m_armsSys;
    Button m_menuB; //メニューボタン。装備リセット系などで呼びたいことが多い

    [SerializeField] GameObject[] m_nomalAttackTimeLines = new GameObject[3];

    [SerializeField] GameObject m_battleUICanvas;
    [SerializeField] GameObject m_resultUICanvas;

    [SerializeField] GameObject m_tempEnemyHPSlider;
    List<EnemyStatusAlfa> m_enemyInstanceList = new List<EnemyStatusAlfa>();
    List<Slider> m_enemyHPBarList = new List<Slider>();
    List<EnemyBattleAIBase> m_enemyAIList = new List<EnemyBattleAIBase>();
    List<GameObject> m_enemyTargettingMarkerList = new List<GameObject>();
    public List<int> m_enemyEncountIDList = new List<int>();
    [SerializeField] Transform[] m_spawnPoints = new Transform[6];
    [SerializeField] GameObject[] m_enemysBattleAnimatorsTemps = new GameObject[2];
    List<GameObject> m_nowEnemysAnimatorList = new List<GameObject>();

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


    [SerializeField] GameObject[] m_kyokuchiEffect = new GameObject[4];
    [SerializeField] GameObject m_whiteFlash;

    List<ChildrenBlackOut> m_kamigakariSiletSettings = new List<ChildrenBlackOut>();

    // Start is called before the first frame update
    void Start()
    {
        //テストエンカウント
        m_enemyEncountIDList.Add(0);

        PlayerStatusSetUP();
        EnemyStatusSetUP(m_enemyEncountIDList);
        StateProgression();

        ShinzuiSet();

        m_menuB = GameObject.Find("MenueButton").GetComponent<Button>();
        m_menuB.onClick.AddListener(ShinzuiSet);
        m_menuB.onClick.AddListener(m_armsSys.MainArmsSetting);
    }

    public void ShinzuiSet()
    {
        for(var i = 0; i < m_shinzuiButtons.Length; i++)
        {
            m_shinzuiButtons[i].transform.SetParent(m_shinzuiPool);
        }
        for(var i = 0; i < PlayerDataAlfa.Instance.m_testInventry.m_mainArms.Count; i++)
        {
            m_shinzuiButtons[PlayerDataAlfa.Instance.m_testInventry.m_mainArms[i].GetID].transform.SetParent(m_shinzuiCircle);
        }
    }

    void Update()
    {
        if(m_nowZone)
        {
            m_playerConSlider.value -= (1f / 10);
        }
    }

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
            var eAnim = Instantiate(m_enemysBattleAnimatorsTemps[ene.m_animatorID]);
            m_nowEnemysAnimatorList.Add(eAnim);
            if(idList.Count == 3)
            {
                eAnim.transform.position = m_spawnPoints[i].position;
            }
            else if(idList.Count == 2)
            {
                eAnim.transform.position = m_spawnPoints[i + 3].position;
            }
            else
            {
                eAnim.transform.position = m_spawnPoints[5].position;
            }
            SetSortingLayer(eAnim.transform, "Chara" + i.ToString());
            m_kamigakariSiletSettings.Add(eAnim.transform.GetChild(0).GetComponent<ChildrenBlackOut>());
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

    void SetSortingLayer(Transform t, string sLayerName)
    {
        if (t.childCount > 0)
        {
            if (t.gameObject.GetComponent<SpriteRenderer>())
            {
                SpriteRenderer s = t.gameObject.GetComponent<SpriteRenderer>();
                s.sortingLayerName = sLayerName;
            }
            for (var i = 0; i < t.childCount; i++)
            {
                if (t.GetChild(i).gameObject.GetComponent<SpriteRenderer>())
                {
                    SpriteRenderer chiS = t.GetChild(i).gameObject.GetComponent<SpriteRenderer>();
                    chiS.sortingLayerName = sLayerName;

                }
                SetSortingLayer(t.GetChild(i), sLayerName);
            }
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

    bool testDodgeControll;
    public void TestDodge()
    {
        if (m_nowPhase == PhaseOnBattle.Input && !testDodgeControll)
        {
            testDodgeControll = true;
            int testDodge = 10;
            float doTime = 1f;
            IEnumerator Progression()
            {
                yield return new WaitForSeconds(doTime);
                m_nowPhase = PhaseOnBattle.Enemy;
                testDodgeControll = false;
                StateProgression();
            }
            StartCoroutine(Progression());
            DOTween.To(() => m_playerDodgSlider.value, x => m_playerDodgSlider.value = x,
                    m_playerDodgSlider.value + testDodge, doTime);
        }
    }

    bool m_AttackControll;
    public void TestAttack()
    {
        if(!(m_nowZone == true && m_zone == ModeOfZone.Kamigakari) && m_nowPhase == PhaseOnBattle.Input && !m_AttackControll && !m_enemyActControll)
        {
            m_nomalAttackTimeLines[PlayerDataAlfa.Instance.m_testInventry.m_mainArms[ArmsSysAlfa.m_carsol].GetMotion].SetActive(true);
        }
    }

    public void NomalAttackDamage(float doTime, bool isLast)
    {
        if (!m_nowZone)
        {
            m_zone = ModeOfZone.Kamigakari;
        }
        m_AttackControll = true;

        int damage = (m_playerAtk + PlayerDataAlfa.Instance.m_testInventry.m_mainArms[ArmsSysAlfa.m_carsol].GetAtk) - m_enemyInstanceList[m_target].m_def / 2;

        if (damage <= 0)
        {
            damage = 1;
        }

        int lessDodg = 10;
        float getConRate = 0.12f;

        if(isLast)
        {
            IEnumerator DeadCheck()
            {
                yield return new WaitForSeconds(doTime);
                m_AttackControll = false;
                IsEnemyDead();
            }

            StartCoroutine(DeadCheck());
        }

        DOTween.To(() => m_enemyHPBarList[m_target].value, x => m_enemyHPBarList[m_target].value = x,
                m_enemyHPBarList[m_target].value - damage, doTime);

        DOTween.To(() => m_playerDodgSlider.value, x => m_playerDodgSlider.value = x,
                m_playerDodgSlider.value - lessDodg, doTime);

        if (!m_nowZone)
        {
            DOTween.To(() => m_playerConSlider.value, x => m_playerConSlider.value = x,
                    m_playerConSlider.value + m_playerConSlider.maxValue * getConRate, doTime);
        }

        StartCoroutine(Shake(doTime, 0.6f)); //画面揺らし
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = m_cameraTF.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            m_cameraTF.position = originalPosition + Random.insideUnitSphere * magnitude;
            elapsed += Time.deltaTime;
            yield return null;
        }
        m_cameraTF.position = originalPosition;
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
        m_menuB.onClick.RemoveListener(ShinzuiSet);
        m_menuB.onClick.RemoveListener(m_armsSys.MainArmsSetting);

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
            case PhaseOnBattle.End:
                break;
        }
    }

    bool m_enemyActControll;
    void EnemysSelectActions()
    {
        if (!m_nowZone)
        {
            m_zone = ModeOfZone.Homura;
        }
        int i = m_enemyHPBarList.Count - 1;
        float doTime = 1.5f;

        StartCoroutine(DeadCheck());

        IEnumerator EneConSetting()
        {
            yield return new WaitForSeconds(doTime);
            m_enemyActControll = false;
        }

        IEnumerator DeadCheck()
        {
            m_enemyActControll = true;
            yield return new WaitForSeconds(doTime);
            if (m_enemyHPBarList[i].value != 0)
            {
                Debug.Log(m_enemyInstanceList[i].m_name + ", スタミナ" + m_enemyInstanceList[i].m_nowStamina);
                m_enemyAIList[i].EnemyActionSelect((int)m_enemyHPBarList[i].value, (int)m_enemyHPBarList[i].maxValue,
                    ref m_enemyInstanceList[i].m_nowStamina, m_enemyInstanceList[i].m_stamina, (int)m_playerDodgSlider.value, (int)m_playerDodgSlider.maxValue);

                //test
                if(!(m_nowZone && m_zone == ModeOfZone.Kamigakari))
                DOTween.To(() => m_playerHPSlider.value, x => m_playerHPSlider.value = x,
                        m_playerHPSlider.value - m_enemyInstanceList[i].m_atk, doTime);
                if(!m_nowZone)
                {
                    float getConRate = 0.12f;
                    DOTween.To(() => m_playerConSlider.value, x => m_playerConSlider.value = x,
                            m_playerConSlider.value + m_playerConSlider.maxValue * getConRate, doTime);
                }

                StartCoroutine(EneConSetting());
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

    public void Kyokuchi()
    {
        if(!m_nowZone && m_playerConSlider.value == m_playerConSlider.maxValue)
        {
            Zone();
        }
        else if(m_nowZone &&　m_playerConSlider.value == 0)
        {
            ZoneEnd();
        }
    }

    void Zone()
    {
        m_whiteFlash.SetActive(true);
        m_nowZone = true;

        switch (m_zone)
        {
            case ModeOfZone.None:
                Debug.LogWarning("極致になってねーのにZone()呼ばれたぞ");
                break;
            case ModeOfZone.Kamigakari:
                m_kyokuchiEffect[0].SetActive(true);
                for(var i = 0; i < m_kamigakariSiletSettings.Count; i++)
                {
                    m_kamigakariSiletSettings[i].Kamigakari();
                }
                break;
            case ModeOfZone.Homura:
                m_kyokuchiEffect[1].SetActive(true);
                break;
            case ModeOfZone.Meikyoushisui:
                m_kyokuchiEffect[2].SetActive(true);
                break;
            case ModeOfZone.Deep:
                m_kyokuchiEffect[3].SetActive(true);
                break;
        }
    }

    void ZoneEnd()
    {
        m_whiteFlash.SetActive(true);
        m_nowZone = false;

        switch (m_zone)
        {
            case ModeOfZone.None:
                Debug.LogWarning("極致になってねーのにZone()呼ばれたぞ");
                break;
            case ModeOfZone.Kamigakari:
                m_kyokuchiEffect[0].SetActive(false);
                for(var i = 0; i < m_kamigakariSiletSettings.Count; i++)
                {
                    m_kamigakariSiletSettings[i].TimeIsBack();
                }
                break;
            case ModeOfZone.Homura:
                m_kyokuchiEffect[1].SetActive(false);
                break;
            case ModeOfZone.Meikyoushisui:
                m_kyokuchiEffect[2].SetActive(false);
                break;
            case ModeOfZone.Deep:
                m_kyokuchiEffect[3].SetActive(false);
                break;
        }
    }
}
