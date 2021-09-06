using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hissatu_2 : HissatuBase
{
    [SerializeField] int m_healingPower = 10;

    public override void Activate()
    {
        FindObjectOfType<Player>().Jikokanri(m_healingPower);
    }
}
