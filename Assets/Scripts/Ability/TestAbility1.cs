using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAbility1 : AbilityBase
{
    protected override void AbilityPlayer()
    {
        Debug.Log("第二のアビリティを解放");
    }
}