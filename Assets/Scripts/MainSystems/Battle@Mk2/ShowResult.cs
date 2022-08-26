using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowResult : MonoBehaviour
{
    [SerializeField] Text m_moneyText;
    [SerializeField] Text m_tpText;
    [SerializeField] Text m_expText;
    [SerializeField] Transform m_getItemContenna;
    [SerializeField] GameObject m_getItemTemp;

    [SerializeField] string[] m_testGetItemNames = new string[6];
    int m_itemCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        int exp = 1000;
        int tp = 1;
        int kane = 1980;
        m_expText.text = $"経験値 +{exp}exp";
        m_tpText.text = $"技量　 +{tp}tp";
        m_moneyText.text = $"報酬金 +{kane}円";
        StartCoroutine(ItemNaraberuCol(m_testGetItemNames[m_itemCount]));
    }

    void ShowItem()
    {
    }

    IEnumerator ItemNaraberuCol(string name)
    {
        yield return new WaitForSeconds(0.3f);
        var item = Instantiate(m_getItemTemp, m_getItemContenna);
        item.SetActive(true);
        Text itemName = item.transform.Find("Name").GetComponent<Text>();
        itemName.text = name;
        m_itemCount++;
        if(m_itemCount <= m_testGetItemNames.Length - 1)
        {
            StartCoroutine(ItemNaraberuCol(m_testGetItemNames[m_itemCount]));
        }
    }
}
