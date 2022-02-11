using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] GameObject m_inbentry = default; //インベントリのパネル

    [SerializeField] List<GameObject> m_otherUI = new List<GameObject>(); //攻撃系ボタンとか

    bool m_flg = false;

    static int? itemKarsol = null; //選択中のアイテムを指し示すカーソル変数

    private void Start()
    {
        itemKarsol = null;
        m_inbentry.SetActive(false);
    }

    public void Inbentry()
    {
        if(m_flg == false)
        {
            Open();
            m_flg = true;
        }
        else
        {
            Close();
            m_flg = false;
        }
    }


    void Open()
    {
        m_inbentry.SetActive(true);

        foreach (var x in m_otherUI)
        {
            x.SetActive(false);
        }
    }
    void Close()
    {
        m_inbentry.SetActive(false);

        foreach (var x in m_otherUI)
        {
            x.SetActive(true);
        }
    }
}
