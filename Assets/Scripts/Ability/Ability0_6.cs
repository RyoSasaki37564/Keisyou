using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability0_6 : AbilityBase
{
    //九字龍撃印　烈

    protected override void AbilityPlayer()
    {
        PlayerDataAlfa.Instance.AbilityActivate(0, 6);
    }
}
