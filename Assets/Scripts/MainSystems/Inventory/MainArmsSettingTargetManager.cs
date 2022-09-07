using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainArmsSettingTargetManager : MonoBehaviour
{
    [SerializeField] RectTransform m_nowSettingTarget;

    public void TargetMarkerSetting(RectTransform rt)
    {
        m_nowSettingTarget.SetParent(rt);
        m_nowSettingTarget.anchoredPosition = Vector2.zero;
        m_nowSettingTarget.sizeDelta = rt.sizeDelta;
    }
}
