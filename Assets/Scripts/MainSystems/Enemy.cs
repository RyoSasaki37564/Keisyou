using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public static List<Enemy> m_enemies = new List<Enemy>();

    /// <summary> エンカウントした敵のIDを格納するリスト </summary>
    public static List<int> m_encountEnemyID = new List<int>();

    /// <summary> Canvas下にある敵UI格納用親オブジェクト </summary>
    [SerializeField] GameObject m_enemyObjectsInCanvas = default;

    [SerializeField] GameObject[] m_enemyUI = default; //敵体力バーなどの元

    public Slider m_enemyHPSL = default;

    [SerializeField] Text m_diaLog = default;

    bool m_moveStopper = false;

    public static bool m_isRyugekiChance = false; //龍撃チャンス

    [SerializeField] GameObject m_RedShutyuSen = default;

    [SerializeField] GameObject m_PALLY_TEST_BOTTUN = default; //パリィ処理のテスト用ボタン

    [SerializeField] Animator m_anim = default;

    bool m_attackAnimStopper = false;

    [SerializeField] GameObject m_ryugekiBottun = default;

    [SerializeField] SkillOfJiga m_jiga = default; //自我の使用状態を見るために参照

    [SerializeField] SkillOfInga m_inga = default;

    [SerializeField] Ryugeki m_ryugekiCS = default;

    bool m_refactorer = false;

    // Start is called before the first frame update
    void Start()
    {
        m_enemies.Clear();

        m_jiga.m_canUse = false;

        m_RedShutyuSen.SetActive(false);

        m_ryugekiBottun.SetActive(false);

        //MonoBehaviourを継承したクラスではListの初期化にコンストラクタが使えないらしい。ので、ここで初期化命令を行う。
        EnemyStuts.m_enemiesStuts = new List<EnemyStuts>();

        //テスト用エンカウント
        m_encountEnemyID.Clear();
        m_encountEnemyID.Add(0);

        EnemyGenerate();

    }

    // Update is called once per frame
    void Update()
    {
        //攻撃処理
        if (BattleManager._theTurn == BattleManager.Turn.EnemyTurn)
        {
            if(Player.Instance.DeadOrAlive() == true ||
                EnemyStuts.m_enemiesStuts[Target.m_tergetNum].DeadOrAlive() == true)
            {
                BattleManager._theTurn = BattleManager.Turn.BattleEnd;
            }
            else
            {
                if (m_moveStopper == false)
                {
                    StartCoroutine(NomalEnemyTurn());
                    m_moveStopper = true;
                }
            }
        }

        /*
        //敵インスタンスリストはstatic、死亡したタイミングでリストから消去
        if(BattleManager._theTurn == BattleManager.Turn.PlayerTurn)
        {
            for (var i = 0; i < EnemyStuts.m_enemiesStuts.Count; i++)
            {
                if (EnemyStuts.m_enemiesStuts[i].m_isDead)
                {
                    EnemyStuts.m_enemiesStuts.Remove(EnemyStuts.m_enemiesStuts[i]);
                }

            }
        }
        */
    }

    private void LateUpdate()
    {
        switch (BattleManager._theTurn)
        {
            case BattleManager.Turn.AwakeTurn:
                break;

            case BattleManager.Turn.InputTurn:
                if(m_refactorer == false)
                {
                    //悪魔に魂売った
                    m_enemies[Target.m_tergetNum].m_enemyHPSL.value = EnemyStuts.m_enemiesStuts[Target.m_tergetNum].m_currentHP;
                    m_refactorer = true;
                    Debug.LogError(m_enemies[Target.m_tergetNum].m_enemyHPSL.value + "反省しろよ");
                }
                break;

            case BattleManager.Turn.PlayerTurn:
                if(Ryugeki.m_isHitRyugeki == true)
                {
                    m_anim.SetBool("IsDamaged", true);
                }
                if (m_refactorer == true)
                {
                    //悪魔に魂売った
                    m_enemies[Target.m_tergetNum].m_enemyHPSL.value = EnemyStuts.m_enemiesStuts[Target.m_tergetNum].m_currentHP;
                    m_refactorer = false;
                    Debug.LogError(m_enemies[Target.m_tergetNum].m_enemyHPSL.value + "反省しろよ");
                }
                break;
            case BattleManager.Turn.EnemyTurn:
                if(m_attackAnimStopper == false)
                {
                    m_anim.SetInteger("AttackMotion1", 1);
                    m_attackAnimStopper = true;
                }
                m_anim.SetBool("IsDamaged", false);
                break;

            case BattleManager.Turn.TurnEnd:
                m_attackAnimStopper = false;
                m_anim.SetBool("IsDamaged", false);
                break;

            case BattleManager.Turn.BattleEnd:
                m_anim.SetBool("IsDead", true);
                break;
        }
    }

    IEnumerator NomalEnemyTurn()
    {
        for (var i = 0; i < EnemyStuts.m_enemiesStuts.Count; i++)
        {
            int dogeJadge = Random.Range(0, 100);
            if(m_jiga.m_zettaiKaihi == true)
            {
                m_diaLog.text =  "自我による回避 ▽";

                yield return new WaitForSeconds(1.5f);
                m_diaLog.text = "";

                m_jiga.m_zettaiKaihi = false;
            }
            else if (dogeJadge < Player.Instance.m_currentDogePower)
            {
                m_diaLog.text = EnemyStuts.m_enemiesStuts[i].m_enemyName + "の攻撃を回避 ▽";

                yield return new WaitForSeconds(1.5f);
                m_diaLog.text = "";
            }
            else
            {
                m_diaLog.text = EnemyStuts.m_enemiesStuts[i].m_enemyName + "の攻撃　▽";
                m_RedShutyuSen.SetActive(true);

                if(Player.Instance.m_currentConcentlate > 10) //集中力が10以上の時
                {
                    //三割でパリィチャンス発生
                    int rand = Random.Range(0, 10);// Random.Range(0, 10);
                    if (rand < 10)//テスト中につき確定パリィチャンス
                    {
                        //  テストが終わったらこのボタンに関連する行は消すこと
                        m_PALLY_TEST_BOTTUN.SetActive(true);

                        yield return new WaitForSeconds(1.5f);
                        if (m_PALLY_TEST_BOTTUN.activeSelf == true)
                        {
                            m_diaLog.text = "";
                            Player.Instance.Damage(EnemyStuts.m_enemiesStuts[i].m_attack, false);
                            m_RedShutyuSen.SetActive(false);

                            IngaOho();

                            m_PALLY_TEST_BOTTUN.SetActive(false);
                        }
                        else
                        {
                            m_diaLog.text = "";
                            m_RedShutyuSen.SetActive(false);
                        }
                    }
                }
                else
                {
                    yield return new WaitForSeconds(1.5f);
                    m_diaLog.text = "";
                    Player.Instance.Damage(EnemyStuts.m_enemiesStuts[i].m_attack, false);
                    m_RedShutyuSen.SetActive(false);

                    IngaOho();
                }
            }
        }
        BattleManager._theTurn = BattleManager.Turn.TurnEnd;
        m_moveStopper = false;
    }

    //パリィ処理のテスト用
    public void PallyTest()
    {
        m_isRyugekiChance = true;
        //パリィは強制的にプレイヤーターンにする
        m_anim.SetBool("IsDamaged", false);
        m_anim.SetInteger("AttackMotion1", 0);
        BattleManager._theTurn = BattleManager.Turn.PlayerTurn;
        m_ryugekiBottun.SetActive(true);

        m_PALLY_TEST_BOTTUN.SetActive(false);

        StartCoroutine(Pallied());
        StartCoroutine(TimeNomalizer());
    }

    IEnumerator Pallied()
    {
        yield return new WaitForSeconds(0.06f);
        //パリィしたとして、龍撃を行えたかどうかで処理を分ける
        if (Ryugeki.m_isHitRyugeki == true)
        {
            Debug.Log("龍撃ヒット");
            yield break;
        }
        else
        {
            m_isRyugekiChance = false;
            m_ryugekiBottun.SetActive(false);

            Debug.Log("龍撃しませんでした");
        }
    }

    IEnumerator TimeNomalizer()
    {
        Time.timeScale = 0.01f;
        yield return new WaitForSeconds(0.03f);
        Time.timeScale = 1f;
    }

    void IngaOho()
    {
        //因果応報
        if (m_inga.m_ingaOho == true && Player.Instance.m_isDead == false && Player.Instance.m_currentHP <= Player.Instance.m_maxHP * 0.5f)
        {
            m_isRyugekiChance = true;
            m_anim.SetInteger("AttackMotion1", 0);
            BattleManager._theTurn = BattleManager.Turn.PlayerTurn;
            m_ryugekiBottun.SetActive(true);
            m_ryugekiCS.RyugekiNoKamae();
            m_inga.m_ingaOho = false;
        }
    }

    public void EnemyGenerate()
    {
        if(GameManager.Instance)
        {
            //エンカウントした敵の数だけ敵の実体を作る
            for (var i = 0; i < m_encountEnemyID.Count; i++)
            {
                EnemyStuts eneStuts = EnemyStuts.EnemyStutsGenerate(i);
                //UIを生成してパラメータを反映
                var x = Instantiate(m_enemyUI[0], m_enemyUI[0].transform.position,
                    m_enemyObjectsInCanvas.transform.rotation);
                x.transform.SetParent(m_enemyObjectsInCanvas.transform);
                x.transform.localScale = new Vector3(0.8f, 0.8f, 1); //これ入れないとHPバーが世界を埋め尽くすほどバカデカくなるにょ。

                Enemy ene = new Enemy();

                ene.m_enemyHPSL = x.GetComponentInChildren<Slider>();
                ene.m_enemyHPSL.maxValue = eneStuts.m_maxHP;
                eneStuts.m_currentHP = eneStuts.m_maxHP;
                Debug.Log("現在の敵体力" + eneStuts.m_currentHP);
                ene.m_enemyHPSL.value = eneStuts.m_currentHP;

                Text enemyNameText = x.GetComponentInChildren<Text>();

                EnemyStuts.m_enemiesStuts.Add(eneStuts);
                enemyNameText.text = EnemyStuts.m_enemiesStuts[i].m_enemyName;
                m_enemies.Add(ene);
            }
            m_enemyUI[0].SetActive(false);
        }
        else
        {
            Debug.LogError("ゲームマネージャー行方不明");
        }
    }
}
