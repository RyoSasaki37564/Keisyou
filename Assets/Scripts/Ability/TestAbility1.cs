using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAbility1 : AbilityBase
{
    private void Start()
    {
        CanActivateTrue();
    }

    public override bool CanActivateTrue()
    {
        return m_canActivate = true;
    }

    protected override void AbilityPlayer()
    {
        Debug.Log("第二のアビリティを解放");
    }
}