using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbilityBase : MonoBehaviour
{
    public enum ActivateState
    {
        Lock,
        Unlockable,
        Active,
    }
    /*[System.NonSerialized]*/ public ActivateState m_nowState = ActivateState.Lock;
    [System.NonSerialized] protected bool m_canActivate = false;

    [SerializeField] public AbilityBase[] m_nextAbilitys = new AbilityBase[1];

    Button m_thisButton;
    [SerializeField] Button m_agreeButton;
    [SerializeField] GameObject m_selectPanel;

    [SerializeField] GameObject m_effect;

    [SerializeField] Text m_flavorText;
    [SerializeField, TextArea(10, 3)] string m_setumei;
    [SerializeField] Text m_nameText;
    [SerializeField] string m_name;

    private void Awake()
    {
        m_thisButton = GetComponent<Button>();
        m_thisButton.onClick.AddListener(OpenOrCloseManue);
        if(m_nowState != ActivateState.Active)
        {
            m_effect.SetActive(false);
        }

    }

    public void OpenOrCloseManue()
    {
        if (m_nowState != ActivateState.Active)
        {
            if (m_selectPanel.activeSelf == false)
            {
                m_agreeButton.onClick.AddListener(Activate);
                m_selectPanel.SetActive(true);
            }
            else
            {
                m_agreeButton.onClick.RemoveListener(Activate);
                m_selectPanel.SetActive(false);
            }
        }

        m_nameText.text = m_name;
        m_flavorText.text = m_setumei;
    }

    public void Activate()
    {
        if(m_canActivate && m_nowState == ActivateState.Unlockable)
        {
            AbilityPlayer();
            NextUnlockablize();
            m_effect.SetActive(true);
            m_nowState = ActivateState.Active;
        }
        else
        {
            Debug.Log($"m_canActivate: {m_canActivate},m_nowState: {m_nowState}, 貴様風情には解放できぬわ!");
        }
        OpenOrCloseManue();
    }

    void NextUnlockablize()
    {
        if(m_nextAbilitys.Length > 0)
        {
            foreach (var a in m_nextAbilitys)
            {
                a.m_nowState = ActivateState.Unlockable;
            }
        }
    }

    protected abstract void AbilityPlayer();

    public abstract bool CanActivateTrue();
}
