using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThisOff : MonoBehaviour
{
    [SerializeField] Animator m_enemyAnim = default;

    [SerializeField] List<GameObject> m_playerCommandsUI = new List<GameObject>(); //ボタンたち

    public void ThisOffMetthod()
    {
        this.gameObject.SetActive(false);

        m_enemyAnim.SetBool("IsDamaged", false);

        foreach (var x in m_playerCommandsUI)
        {
            x.SetActive(true);
        }
    }
}
