﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAbility1 : AbilityBase
{
    //回避の心得　足捌き

    protected override void AbilityPlayer()
    {
        PlayerDataAlfa.Instance.DodgeAbilitiesActivate(0);
    }
}