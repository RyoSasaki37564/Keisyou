﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability0_4 : AbilityBase
{
    //九字龍撃印　皆

    private void Start()
    {
        m_nowState = ActivateState.Unlockable;
        Select();
    }

    protected override void AbilityPlayer()
    {
        //PlayerDataAlfa.Instance.AbilityActivate(0, 4);

        for(var i = 0; i < 9; i++)
        {
            PlayerDataAlfa.Instance.AbilityActivate(0, i);
        }
    }
}
