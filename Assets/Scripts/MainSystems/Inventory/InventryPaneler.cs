using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventryPaneler : MonoBehaviour
{
    [SerializeField] Transform m_itemInventryContenna;
    [SerializeField] Transform m_armInventryContenna;
    [SerializeField] GameObject[] m_mainArms = new GameObject[5];
    [SerializeField] Transform m_itemShortCutContenna;
    [SerializeField] GameObject m_itemTemp;
    [SerializeField] GameObject m_armTemp;

    [SerializeField] GameObject[] m_menuPanels = new GameObject[5]; // 0 = 装備、1 = 道具、2 = 屠龍具、3 = 貴重品、4 = 1~3の親

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

            /*
            if(i == 0)
            {
                idp.ShowData();
            }*/

            if(PlayerDataAlfa.Instance.m_testInventry.m_itemInventry[i].GetCount > 0)
            {
                t.SetActive(true);
            }
        }

        //テスト獲得
        PlayerDataAlfa.Instance.m_testInventry.m_armsInventry[0].InToTheHand();
        PlayerDataAlfa.Instance.m_testInventry.m_armsInventry[1].InToTheHand();

        for (var i = 0; i < PlayerDataAlfa.Instance.m_testInventry.m_armsInventry.Length; i++)
        {
            var a = Instantiate(m_armTemp, m_armInventryContenna);
            Text name = a.transform.Find("Name").GetComponent<Text>();
            name.text = PlayerDataAlfa.Instance.m_testInventry.m_armsInventry[i].GetName;
            Text count = a.transform.Find("Count").GetComponent<Text>();
            if(PlayerDataAlfa.Instance.m_testInventry.m_armsInventry[i].GetIsHave)
            {
                count.text = "× 1";
            }

            ArmDataPaneler adp = a.GetComponent<ArmDataPaneler>();
            adp.m_id = i;

            if (PlayerDataAlfa.Instance.m_testInventry.m_armsInventry[i].GetIsHave)
            {
                a.SetActive(true);
            }
        }

        for (var i = 1; i < m_menuPanels.Length; i++)
        {
            m_menuPanels[i].SetActive(false);
        }
        SelectAndOpen(0);

        MainArmsView();
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

    public void SelectAndOpen(int num)
    {
        if(num == 4) { Debug.LogError("それ根元や"); return; }

        if(!m_menuPanels[num].activeSelf)
        {
            for (var i = 0; i < m_menuPanels.Length; i++)
            {
                if(i != num)
                {
                    m_menuPanels[i].SetActive(false);
                }
            }

            if(num != 0)
            {
                //0以外はスクロールビュー内にあり親オブジェクトを共有しているので、親を起こす
                m_menuPanels[4].SetActive(true);
            }
            m_menuPanels[num].SetActive(true);
        }
    }

    public void MainArmsView()
    {
        for(var i = 0; i < PlayerDataAlfa.Instance.m_testInventry.m_mainArms.Count; i++)
        {

            m_mainArms[PlayerDataAlfa.Instance.m_testInventry.m_mainArms[i].GetID].transform.SetAsLastSibling();
            m_mainArms[PlayerDataAlfa.Instance.m_testInventry.m_mainArms[i].GetID].SetActive(true);
        }
    }
}
