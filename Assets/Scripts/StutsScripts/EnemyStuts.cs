using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStuts : BattleChara
{
    public static List<EnemyStuts> m_enemiesStuts; //戦闘ごとに設定された数の敵実体を格納

    public string m_enemyName = default; //おなまえ

    public int m_statmina { get; set; }
    public int m_type { get; set; }

    /// <summary> なまえ　たいりょく　こうげき　ぼうぎょ すたみな</summary>
    public EnemyStuts(string name, float hp, float atk, float def, int stamina, int type)
    {
        this.m_enemyName = name;
        this.m_maxHP = hp;
        this.m_attack = atk;
        this.m_deffence = def;
        this.m_statmina = stamina;
        this.m_type = type;
    }

    public static EnemyStuts EnemyStutsGenerate(int id)
    {
        if (GameManager.Instance)
        {
            //マスターからこのエネミーのステータスを取得
            EnemyStuts ene = new EnemyStuts(GameManager.Instance.m_enemyMaster[Enemy.m_encountEnemyID[id]].e_name,
                GameManager.Instance.m_enemyMaster[Enemy.m_encountEnemyID[id]].e_hp,
                GameManager.Instance.m_enemyMaster[Enemy.m_encountEnemyID[id]].e_attack,
                GameManager.Instance.m_enemyMaster[Enemy.m_encountEnemyID[id]].e_deffence,
                GameManager.Instance.m_enemyMaster[Enemy.m_encountEnemyID[id]].e_stamina,
                GameManager.Instance.m_enemyMaster[Enemy.m_encountEnemyID[id]].e_type);

            Debug.Log("Enemy LogIn");

            return ene;
        }
        else
        {
            Debug.LogError("ゲームマネージャー行方不明");
            return null;
        }
    }
}