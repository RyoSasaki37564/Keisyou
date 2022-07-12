using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneTextTest : MonoBehaviour
{
    [SerializeField] string m_charaName;
    [SerializeField, TextArea(1, 3)] string[] m_scenarios = new string[3];

    private void Start()
    {
        ScenarioManager.Instance.m_name = m_charaName;
        ScenarioManager.Instance.m_scenariosArray = m_scenarios;
    }
}
