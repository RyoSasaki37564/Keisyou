﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability0_2 : AbilityBase
{
    //九字龍撃印　闘

    protected override void AbilityPlayer()
    {
        PlayerDataAlfa.Instance.AbilityActivate(0, 2);
    }
}
