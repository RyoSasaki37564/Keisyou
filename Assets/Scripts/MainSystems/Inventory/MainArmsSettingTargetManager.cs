using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainArmsSettingTargetManager : MonoBehaviour
{
    [SerializeField] RectTransform m_nowSettingTarget;
    public Toryugu m_nowTargetArm;

    public void FirstSetting()
    {
        for(var i = 0; i < transform.GetChild(0).childCount; i++)
        {
            if(transform.GetChild(0).GetChild(i).gameObject.activeSelf)
            {
                m_nowSettingTarget.SetParent(transform.GetChild(0).GetChild(i).transform);
                m_nowSettingTarget.SetAsLastSibling();
                m_nowSettingTarget.anchoredPosition = Vector2.zero;
                m_nowSettingTarget.sizeDelta = Vector2.zero;
                return;
            }
        }
    }

    public void TargetMarkerSetting(RectTransform rt)
    {
        m_nowSettingTarget.SetParent(rt);
        m_nowSettingTarget.anchoredPosition = Vector2.zero;
        m_nowSettingTarget.sizeDelta = rt.sizeDelta;
        m_nowSettingTarget.SetAsLastSibling();
    }
}
