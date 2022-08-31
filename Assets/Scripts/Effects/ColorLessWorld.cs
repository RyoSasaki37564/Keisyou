using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ColorLessWorld : MonoBehaviour
{
    [SerializeField] PostProcessVolume m_ppv;

    /// <summary>
    /// セットしているポストプロッセシングが色彩落としである前提
    /// </summary>
    public void WhiteWorld()
    {
        m_ppv.weight = 1;
    }

    public void NomalizedWorld()
    {
        m_ppv.weight = 0;
    }
}
