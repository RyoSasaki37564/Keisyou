using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability0_0 : AbilityBase
{
    //九字龍撃印　臨

    protected override void AbilityPlayer()
    {
        PlayerDataAlfa.Instance.AbilityActivate(0, 0);
    }
}
