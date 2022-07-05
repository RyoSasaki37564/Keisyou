using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEventOchiai : FieldEventUnit
{
    [SerializeField] string m_charaName;
    [SerializeField, TextArea(1, 3)] string[] m_scenarios = new string[3];

    [SerializeField, Tooltip("0:↓, 1:↑, 2:→, 3:←")] Sprite[] m_angleSprites = new Sprite[4];
    SpriteRenderer m_spriteRenderer;

    private void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void FieldEvent(GameObject caller)
    {
        if(this.transform.position.x < caller.transform.position.x)
        {
            m_spriteRenderer.sprite = m_angleSprites[2];
        }
        else if(this.transform.position.x > caller.transform.position.x)
        {
            m_spriteRenderer.sprite = m_angleSprites[3];
        }
        else if(this.transform.position.y < caller.transform.position.y)
        {
            m_spriteRenderer.sprite = m_angleSprites[1];
        }
        else
        {
            m_spriteRenderer.sprite = m_angleSprites[0];
        }

        ScenarioManager.Instance.m_name = m_charaName;
        ScenarioManager.Instance.m_scenariosArray = m_scenarios;
        ScenarioManager.Instance.ScenarioModeON();
    }
}