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
    public ActivateState m_nowState = ActivateState.Lock;

    [SerializeField] public AbilityBase[] m_nextAbilitys = new AbilityBase[1];
    [SerializeField] Image[] m_pipes = new Image[0];

    Button m_thisButton;
    [SerializeField] Button m_agreeButton;
    [SerializeField] GameObject m_selectPanel;

    [SerializeField] GameObject m_activateMarker;

    [SerializeField] Text m_flavorText;
    [SerializeField, TextArea(20, 3)] string m_setumei;
    [SerializeField] Text m_nameText;
    [SerializeField] string m_name;

    [SerializeField] int m_costTP;

    private void OnEnable()
    {
        Debug.Log("アクティブ");
        if(m_thisButton == null)
        {
            Debug.Log("取得");
            m_thisButton = GetComponent<Button>();

            m_thisButton.onClick.AddListener(OpenOrCloseManue);
            if (m_nowState != ActivateState.Active)
            {
                m_activateMarker.SetActive(true);
            }
            if (m_nowState != ActivateState.Active && m_pipes.Length > 0)
            {
                m_activateMarker.SetActive(false);
                foreach (var i in m_pipes)
                {
                    i.color = new Color(100f / 255, 100f / 255, 100f / 255);
                }
            }
        }
    }

    private void OnDisable()
    {
        if(m_selectPanel.activeSelf)
        {
            m_selectPanel.SetActive(false);
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
        m_flavorText.text = $"\n解放必要技量：{m_costTP}\n\n" + m_setumei;

        //これしないと正常にリサイジングしてくれない
        m_flavorText.gameObject.SetActive(false);
        m_flavorText.gameObject.SetActive(true);
    }

    public void Activate()
    {
        Debug.Log($"cost{m_costTP}, now{PlayerDataAlfa.Instance.m_tp}, {m_nowState}");
        if(m_costTP <= PlayerDataAlfa.Instance.m_tp && m_nowState == ActivateState.Unlockable)
        {
            PlayerDataAlfa.Instance.m_tp -= m_costTP;
            AbilityPlayer();
            NextUnlockablize();
            m_activateMarker.SetActive(true);
            m_nowState = ActivateState.Active;
            if(m_pipes.Length > 0)
            {
                foreach (var i in m_pipes)
                {
                    i.color = new Color(173f / 255, 222f / 255, 231f / 255);
                }
            }
            OpenOrCloseManue();
        }
        else
        {
            //ダメな音を鳴らす
        }
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
}
