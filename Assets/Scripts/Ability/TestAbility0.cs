using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAbility0 : AbilityBase
{
    private void Start()
    {
        m_nowState = ActivateState.Unlockable;
        OpenOrCloseManue();
    }

    protected override void AbilityPlayer()
    {
        Debug.Log("最初のアビリティを解放");
    }
}
