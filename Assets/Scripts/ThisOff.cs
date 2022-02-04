using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThisOff : MonoBehaviour
{
    [SerializeField] Animator m_enemyAnim = default;

    public void ThisOffMetthod()
    {
        this.gameObject.SetActive(false);

        m_enemyAnim.SetBool("IsDamaged", false);
    }
}
