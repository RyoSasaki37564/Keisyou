using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hissatu_1 : HissatuBase
{
    [SerializeField] int m_killPower = 999;

    public override void Activate()
    {
        FindObjectOfType<Enemy>().YabaiTaken(m_killPower);
    }
}
