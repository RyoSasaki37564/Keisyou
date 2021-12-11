﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 戦闘シーンにおけるキャラクター全般の始祖クラス。担当：必須共通パラメータ宣言及び体力処理の関数と生死判定
/// </summary>
public class BattleChara : MonoBehaviour
{
    //体力
    public float m_maxHP { get; set; }
    public float m_currentHP { get; set; }

    //攻撃
    public float m_attack { get; set; }

    //防御
    public float m_deffence { get; set; }

    //生死
    public bool m_isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ダメージ処理。防御無視ならばboolをtrueに。
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="isUnDeffencive"></param>
    public virtual void Damage(float damage, bool isUnDeffencive)
    {
        if(isUnDeffencive == false)
        {
            m_currentHP -= damage - (damage * m_deffence);
            m_isDead = DeadOrAlive();
        }
        else
        {
            m_currentHP -= damage;
            m_isDead = DeadOrAlive();
        }
    }

    /// <summary>
    /// 回復処理
    /// </summary>
    /// <param name="medic"></param>
    public virtual void Healing(float medic)
    {
        m_currentHP += medic;
        if(m_currentHP > m_maxHP)
        {
            m_currentHP = m_maxHP;
        }
    }

    /// <summary>
    /// 生死判定
    /// </summary>
    /// <returns></returns>
    public virtual bool DeadOrAlive()
    {
        if (m_currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}