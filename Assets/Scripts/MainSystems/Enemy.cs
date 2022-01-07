using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public static List<Enemy> m_enemies = new List<Enemy>();

    /// <summary> 戦闘画面における敵のビジュアル管理オブジェクト </summary>
    [SerializeField] GameObject[] m_enemyGrahicsBody = default;

    /// <summary> 固有特異行動 </summary>
    [SerializeField] GameObject[] m_enemySpecialMove = default;

    /// <summary> エンカウントした敵のIDを格納するリスト </summary>
    public static List<int> m_encountEnemyID = new List<int>();


    /// <summary> Canvas下にある敵UI格納用親オブジェクト </summary>
    [SerializeField] GameObject m_enemyObjectsInCanvas = default;

    [SerializeField] GameObject[] m_enemyUI = default; //敵体力バーなどの元

    public Slider m_enemyHPSL = default;

    [SerializeField] Text m_diaLog = default;

    bool m_moveStopper = false;

    [SerializeField] GameObject m_RedShutyuSen = default;

    // Start is called before the first frame update
    void Start()
    {
        m_RedShutyuSen.SetActive(false);

        //MonoBehaviourを継承したクラスではListの初期化にコンストラクタが使えないらしい。ので、ここで初期化命令を行う。
        EnemyStuts.m_enemiesStuts = new List<EnemyStuts>();

        //テスト用エンカウント
        m_encountEnemyID.Add(0);
        m_encountEnemyID.Add(0);

        EnemyGenerate();
    }

    // Update is called once per frame
    void Update()
    {
        //攻撃処理
        if (BattleManager._theTurn == BattleManager.Turn.EnemyTurn)
        {
            if(m_moveStopper == false)
            {
                StartCoroutine(NomalEnemyTurn());
                m_moveStopper = true;
            }
        }

        //敵インスタンスはstaticなので、死亡したタイミングで消去
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
    }

    IEnumerator NomalEnemyTurn()
    {
        for (var i = 0; i < EnemyStuts.m_enemiesStuts.Count; i++)
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                int dogeJadge = Random.Range(0, 100);
                if(dogeJadge < Player.Instance.m_currentDogePower)
                {
                    m_diaLog.text = EnemyStuts.m_enemiesStuts[i].m_enemyName +"の攻撃を回避 ▽";
                    yield return new WaitForSeconds(1.5f);
                    m_diaLog.text = "";
                }
                else
                {
                    m_diaLog.text = EnemyStuts.m_enemiesStuts[i].m_enemyName +"の攻撃　▽";
                    m_RedShutyuSen.SetActive(true);
                    yield return new WaitForSeconds(1.5f);
                    m_diaLog.text = "";
                    Player.Instance.Damage(EnemyStuts.m_enemiesStuts[i].m_attack, false);
                    m_RedShutyuSen.SetActive(false);
                }
            }
            else
            {
                m_diaLog.text = EnemyStuts.m_enemiesStuts[i].m_enemyName + "は様子を見ている　▽";
                yield return new WaitForSeconds(1.5f);
                m_diaLog.text = "";
            }
        }
        BattleManager._theTurn++;
        m_moveStopper = false;
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
