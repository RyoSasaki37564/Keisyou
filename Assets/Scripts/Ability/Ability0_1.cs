using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability0_1 : AbilityBase
{
    //九字龍撃印　兵

    protected override void AbilityPlayer()
    {
        PlayerDataAlfa.Instance.AbilityActivate(0, 1);
    }
}
