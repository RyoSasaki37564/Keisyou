using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability0_3 : AbilityBase
{
    //九字龍撃印　者

    protected override void AbilityPlayer()
    {
        PlayerDataAlfa.Instance.AbilityActivate(0, 3);
    }
}
