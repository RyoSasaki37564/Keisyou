using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneTextTest : MonoBehaviour
{
    [SerializeField] string m_charaName;
    [SerializeField, TextArea(1, 3)] string[] m_scenarios = new string[3];

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        ScenarioManager.Instance.m_director = this.gameObject.GetComponent<UnityEngine.Playables.PlayableDirector>();
        ScenarioManager.Instance.m_name = m_charaName;
        ScenarioManager.Instance.m_scenariosArray = m_scenarios;
    }
}
