using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventryPaneler : MonoBehaviour
{
    [SerializeField] Transform m_itemInventryContenna;
    [SerializeField] GameObject m_itemTemp;

    private void Start()
    {
        for(var i = 0; i < PlayerDataAlfa.Instance.m_testInventry.m_itemInventry.Length; i++)
        {
            var t = Instantiate(m_itemTemp, m_itemInventryContenna);
            Text name = t.transform.Find("Name").GetComponent<Text>();
            name.text = PlayerDataAlfa.Instance.m_testInventry.m_itemInventry[i].GetName;
            Text count = t.transform.Find("Count").GetComponent<Text>();
            count.text = "× " + PlayerDataAlfa.Instance.m_testInventry.m_itemInventry[i].GetCount.ToString();
            ItemDataPaneler idp = t.GetComponent<ItemDataPaneler>();
            idp.m_id = i;
            if(i == 0)
            {
                idp.ShowData();
            }

            if(PlayerDataAlfa.Instance.m_testInventry.m_itemInventry[i].GetCount > 0)
            {
                t.SetActive(true);
            }
        }
    }

    public void ItemCountSet()
    {
        for (var i = 0; i < PlayerDataAlfa.Instance.m_testInventry.m_itemInventry.Length; i++)
        {
            var t = m_itemInventryContenna.transform.GetChild(i+1).gameObject; //テンプレート飛ばし
            Text name = t.transform.Find("Name").GetComponent<Text>();
            name.text = PlayerDataAlfa.Instance.m_testInventry.m_itemInventry[i].GetName;
            Text count = t.transform.Find("Count").GetComponent<Text>();
            count.text = "× " + PlayerDataAlfa.Instance.m_testInventry.m_itemInventry[i].GetCount.ToString();

            if (PlayerDataAlfa.Instance.m_testInventry.m_itemInventry[i].GetCount > 0)
            {
                if(!t.activeSelf)
                {
                    t.SetActive(true);
                }
            }
            else
            {
                if (t.activeSelf)
                {
                    t.SetActive(false);
                }
            }
        }
    }
}
