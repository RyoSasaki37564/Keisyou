using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyunokoAnimEV : MonoBehaviour
{
    [SerializeField] Animator m_anim = default;

    public void WillAttack()
    {
        m_anim.SetInteger("AttackMotion1", 2);
    }
    public void Attacked_1()
    {
        m_anim.SetInteger("AttackMotion1", 3);
    }
}
