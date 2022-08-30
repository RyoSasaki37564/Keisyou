using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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

        GetResultTemp(kane, exp, tp);
        StartCoroutine(ItemNaraberuCol(m_testGetItemNames[m_itemCount], 1));
    }

    IEnumerator ItemNaraberuCol(string name, int count)
    {
        yield return new WaitForSeconds(0.3f);
        var item = Instantiate(m_getItemTemp, m_getItemContenna);
        item.SetActive(true);
        Text itemName = item.transform.Find("Name").GetComponent<Text>();
        Text itemCount = item.transform.Find("Count").GetComponent<Text>();
        itemName.text = name;
        itemCount.text = $"× {count}";
        m_itemCount++;
        if(m_itemCount <= m_testGetItemNames.Length - 1)
        {
            StartCoroutine(ItemNaraberuCol(m_testGetItemNames[m_itemCount], 1));
        }
    }

    void GetResultTemp(int resultMoney, int resultExp, int resultTp)
    {
        m_moneyText.text = $"報酬金 +{resultMoney}円";
        m_expText.text = $"経験値 +{resultMoney}exp";
        m_tpText.text = $"技量　 +{resultTp}tp";
        PlayerDataAlfa.Instance.m_money += resultMoney;
        PlayerDataAlfa.Instance.m_exp += resultExp;
        PlayerDataAlfa.Instance.m_tp += resultTp;
    }

    public void ResultEnd()
    {
        SceneManager.UnloadSceneAsync("BattleAlfa");
        PlayerDataAlfa.Instance.m_encountData.ObjectsOn();
    }
}
