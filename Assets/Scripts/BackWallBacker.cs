using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackWallBacker : MonoBehaviour
{
    [SerializeField] Animator m_back = default;

    public void Backer()
    {
        m_back.SetBool("IsApproach", false);
    }
}
