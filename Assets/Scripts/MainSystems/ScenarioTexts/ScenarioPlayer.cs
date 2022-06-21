using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 単純にテキスト送りがしたいときのためのやつ。
/// 
/// </summary>
public class ScenarioPlayer : MonoBehaviour
{
    [SerializeField] string m_name = "しゃべってるやつの名前";
    [SerializeField, TextArea(1,2)] string m_scenario = "龍滅の刃...?\n" + "龍滅の刃...!?";
    [SerializeField] Text m_speakerNameText = default;
    [SerializeField] Text m_scenarioText = default;
    [SerializeField] float m_oneTakeTime = 0.3f;
    int m_openedCount = 1;

    public void TestLoading()
    {
        m_speakerNameText.text = m_name;
        IchimojiZutsu(m_scenario);
    }

    public void IchimojiZutsu(string naiyou)
    {
        string s = naiyou.Substring(0, m_openedCount);
        if (s[s.Length - 1] == '\n')
        {
            //改行に際して2秒遅くする
            StartCoroutine(IchiJiKiri(m_oneTakeTime + 2, s, naiyou));
        }
        else
        {
            StartCoroutine(IchiJiKiri(m_oneTakeTime, s, naiyou));
        }
    }

    IEnumerator IchiJiKiri(float reloadTime, string bunsyou, string naiyou)
    {
        yield return new WaitForSeconds(reloadTime);
        m_scenarioText.text = bunsyou;
        if(bunsyou != naiyou)
        {
            m_openedCount++;
            IchimojiZutsu(naiyou);
        }
    }
}
