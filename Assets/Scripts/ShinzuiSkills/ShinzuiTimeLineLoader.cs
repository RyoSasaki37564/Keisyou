using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShinzuiTimeLineLoader : MonoBehaviour
{
    public static int? skillEffectsID = null; //スキル演出呼び出し用のカーソル

    [SerializeField] List<GameObject> m_ensyutuTimeLineList = new List<GameObject>();

    private void Start()
    {
        foreach(var i in m_ensyutuTimeLineList)
        {
            i.SetActive(false);
        }
    }

    public void EnsyutuShiteYannyo()
    {
        if(skillEffectsID != null)
        {
            m_ensyutuTimeLineList[(int)skillEffectsID].SetActive(true);
            skillEffectsID = null;
        }
        else
        {
            Debug.Log("IDが渡されていません");
        }
    }
}
