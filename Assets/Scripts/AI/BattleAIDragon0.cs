using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAIDragon0 : EnemyBattleAIBase
{
    enum PrevCommand
    {
        Non,
        NomalAttack,
        DangerAttack,
        Roar,
        Rest,
    }

    PrevCommand m_preCom = PrevCommand.Non;
    int m_repeatCount = 0;

    string m_non = "Wait";
    string m_morph = "MorphChange";
    string m_attack = "Attack";
    string m_danger = "Danger";
    string m_roar = "Roar";
    string m_rest = "Rest";
    // 

    /// <summary>
    /// このfreePara0はプレイヤーの回避
    /// </summary>
    public override string EnemyActionSelect(int nowHP, int maxHP, ref int nowStamina, int maxStamina, int freePara0, int freePara0Max)
    {
        //froatキャスト必須
        float hpRate = (float)nowHP / (float)maxHP;
        float staminaRate = (float)nowStamina / (float)maxStamina;
        float playerDodgeRate = (float)freePara0 / (float)freePara0Max;

        float wantMorphing = 0;
        float wantNomalAttack = FuzzyLogic.FuzzyReverseGrade(hpRate,0f, 0.9f);
        float wantDangerAttack = FuzzyLogic.FuzzyReverseGrade(hpRate, 0.2f, 0.5f);
        float wantRest = FuzzyLogic.FuzzyReverseGrade(staminaRate, 0.1f, 0.3f);
        float wantRoar = FuzzyLogic.FuzzyGrade(playerDodgeRate, 0.8f, 0.9f);


        if (hpRate <= 0.5f && !m_isMorphChanged)
        {
            wantMorphing = 2f;
            m_isMorphChanged = true;
        }

        float select = Mathf.Max(wantMorphing, wantNomalAttack, wantDangerAttack, wantRest, wantRoar);
        if (select == wantMorphing)
        {
            CommandOfMorphProgression();
            m_repeatCount = 0;
            return m_morph;
        }
        else if (select == wantRoar)
        {
            if (m_preCom == PrevCommand.Roar)
            {
                if (m_repeatCount < 2)
                {
                    CommandOfRoar();
                    return m_roar;
                }
                else
                {
                    CommandOfNomalAttack(ref nowStamina, maxStamina);
                    return m_attack;
                }
            }
            else
            {
                CommandOfRoar();
                return m_roar;
            }
        }

        else if (select == wantNomalAttack)
        {
            if(m_preCom == PrevCommand.NomalAttack)
            {
                int rand = Random.Range(3, 6);
                if (m_repeatCount < rand)
                {
                    CommandOfRoar();
                    return m_roar;
                }
                else
                {
                    CommandOfNomalAttack(ref nowStamina, maxStamina);
                    return m_attack;
                }
            }
            else
            {
                CommandOfNomalAttack(ref nowStamina, maxStamina);
                return m_attack;
            }
        }

        else if (select == wantDangerAttack)
        {
            if (m_preCom == PrevCommand.DangerAttack)
            {
                if (m_repeatCount < 3)
                {
                    CommandOfDangerAttack(ref nowStamina, maxStamina);
                    return m_danger;
                }
                else
                {

                    CommandOfNomalAttack(ref nowStamina, maxStamina);
                    return m_attack;
                }
            }
            else
            {
                CommandOfDangerAttack(ref nowStamina, maxStamina);
                return m_danger;
            }
        }

        else if (select == wantRest)
        {
            CommandOfRest(ref nowStamina, maxStamina);
            return m_rest;
        }

        else
        {
            Debug.Log("それ以外");
            return m_non;
        }
    }

    void CommandOfMorphProgression()
    {
        Debug.Log("形態変化");
        CommandOfRoar();
    }

    void CommandOfNomalAttack(ref int stm, int maxSTm)
    {
        if (m_preCom == PrevCommand.NomalAttack)
        {
            m_repeatCount++;
        }
        else
        {
            m_repeatCount = 0;
        }
        m_preCom = PrevCommand.NomalAttack;
        UseStamina(ref stm, maxSTm, 0.1f);
        Debug.Log("通常攻撃");
    }

    void CommandOfDangerAttack(ref int stm, int maxSTm)
    {
        if (m_preCom == PrevCommand.DangerAttack)
        {
            m_repeatCount++;
        }
        else
        {
            m_repeatCount = 0;
        }
        m_preCom = PrevCommand.DangerAttack;
        UseStamina(ref stm, maxSTm, 0.2f);
        Debug.Log("危険攻撃");
    }

    void CommandOfRoar()
    {
        if (m_preCom == PrevCommand.Roar)
        {
            m_repeatCount++;
        }
        else
        {
            m_repeatCount = 0;
        }
        m_preCom = PrevCommand.Roar;
        Debug.Log("咆哮");
    }

    void CommandOfRest(ref int stamina, int max)
    {
        if (m_preCom == PrevCommand.Rest)
        {
            m_repeatCount++;
        }
        else
        {
            m_repeatCount = 0;
        }
        m_preCom = PrevCommand.Rest;
        stamina = max;
        Debug.Log("スタミナ切れ");
    }

    void UseStamina(ref int eneStamina, int eneStaminaMax, float useRate)
    {
        if (eneStamina - eneStaminaMax * useRate < 0)
        {
            eneStamina = 0;
        }
        else
        {
            eneStamina -= (int)(eneStaminaMax * useRate);
        }
    }
}
