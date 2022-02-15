using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandUp : MonoBehaviour
{
    [SerializeField] GameObject m_UICanvas = default;

    [SerializeField] SEPlay m_SE = default;

    [SerializeField] GameObject m_parent = default;

    public void BattleStart()
    {
        m_SE.MyPlayOneShot();
        m_UICanvas.SetActive(true);
        m_parent.SetActive(false);
    }
}
