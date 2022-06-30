using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEventOchiai : FieldEventUnit
{
    [SerializeField] string m_charaName;
    [SerializeField, TextArea(1, 3)] string[] m_scenarios = new string[3];

    public override void FieldEvent()
    {
        ScenarioManager.Instance.m_name = m_charaName;
        ScenarioManager.Instance.m_scenariosArray = m_scenarios;
        ScenarioManager.Instance.ScenarioModeON();
    }
}