using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBase : MonoBehaviour
{
    [SerializeField] GameObject m_itemPannel = default; //アイテム状態表示パネル
    [SerializeField] Text m_itemName = default;
    [SerializeField] Text m_itemSetumei = default;
    [SerializeField] Text m_itemCount = default;
    [SerializeField] GameObject m_useBottun = default;
    [SerializeField] GameObject m_useBottunParent = default;
    List<GameObject> m_useBottunParentChildren = new List<GameObject>();

    public int m_intCount = 0; //所持数

    public bool m_canUse = false;

    public enum PannelingItemKarsol
    {
        gannyaku,
        amedama,
        musimizu,
        _default,
    }
    static PannelingItemKarsol nowPannelingSkillInfo = PannelingItemKarsol._default;

    // Start is called before the first frame update
    public virtual void Start()
    {
        for (var i = 0; i < m_useBottunParent.transform.childCount; i++)
        {
            m_useBottunParentChildren.Add(m_useBottunParent.transform.GetChild(i).gameObject);
        }
        m_itemPannel.SetActive(false);
        m_useBottun.SetActive(false);
        m_canUse = true;//false;
    }

    public virtual void UseItem()
    {
        m_itemPannel.SetActive(false);
        m_intCount--;
        if (m_intCount == 0)
        {
            m_canUse = false;
        }
    }

    public virtual void Panneler(string name, string setumei, PannelingItemKarsol karsol)
    {
        if (m_itemPannel.activeSelf == false || karsol != nowPannelingSkillInfo)
        {
            m_itemName.text = name;
            m_itemSetumei.text = setumei;
            m_itemCount.text = "x " + m_intCount.ToString();
            foreach (var x in m_useBottunParentChildren)
            {
                x.SetActive(false);
            }
            m_useBottun.SetActive(true);
            m_itemPannel.SetActive(true);
            nowPannelingSkillInfo = karsol;
        }
        else
        {
            m_itemPannel.SetActive(false);
        }
    }
}
