using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability0_7 : AbilityBase
{
    //九字龍撃印　在

    protected override void AbilityPlayer()
    {
        PlayerDataAlfa.Instance.AbilityActivate(0, 7);
    }
}
