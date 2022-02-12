using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shinzui : MonoBehaviour
{
    [SerializeField] GameObject m_circle = default;

    [SerializeField] List<GameObject> m_otherCommands = new List<GameObject>(); //いまアタックボタンしか入っとらん

    [SerializeField] Animator m_ainm = default;

    [SerializeField] GameObject m_setumei = default; //内容説明のやつ 

    bool m_dashiireFlg = false;

    // Start is called before the first frame update
    void Start()
    {
        m_circle.SetActive(false);
    }

    public void Panneling()
    {
        if (m_dashiireFlg == true)
        {
            m_setumei.SetActive(false);
            m_ainm.SetBool("IsShinzui", false);
            foreach (var i in m_otherCommands)
            {
                i.SetActive(true);
            }
            m_dashiireFlg = false;
        }
        else
        {
            m_circle.SetActive(true);
            m_dashiireFlg = true;
            m_ainm.SetBool("IsShinzui", true);
            foreach (var i in m_otherCommands)
            {
                i.SetActive(false);
            }
        }
    }
}
