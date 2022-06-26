using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAbility0 : AbilityBase
{
    public override bool CanActivateTrue()
    {
        return m_canActivate = true;
    }

    protected override void AbilityPlayer()
    {
        Debug.Log("最初のアビリティを解放");
    }
}
