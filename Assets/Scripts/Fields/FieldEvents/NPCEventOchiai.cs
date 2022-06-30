using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEventOchiai : FieldEventUnit
{
    public override void FieldEvent()
    {
        ScenarioManager.Instance.ScenarioModeON();
    }
}