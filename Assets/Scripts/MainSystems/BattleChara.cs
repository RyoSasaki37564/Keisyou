using System.Collections;
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

    /// <summary>
    /// ダメージ処理。防御無視ならばboolをtrueに。
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="isUnDeffencable"></param>
    public virtual float Damage(float damage, bool isUnDeffencable)
    {
        if(isUnDeffencable == false)
        {
            m_currentHP -= damage - (damage * m_deffence);
            m_isDead = DeadOrAlive();

            return damage - (damage * m_deffence);
        }
        else
        {
            m_currentHP -= damage;
            m_isDead = DeadOrAlive();

            return damage;
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
    /// <returns>true == 死, false == 生</returns>
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
