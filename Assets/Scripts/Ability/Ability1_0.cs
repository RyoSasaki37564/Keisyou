using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability1_0 : AbilityBase
{
    //回避の心得　足捌き

    protected override void AbilityPlayer()
    {
        PlayerDataAlfa.Instance.AbilityActivate(1, 0);
    }
}

