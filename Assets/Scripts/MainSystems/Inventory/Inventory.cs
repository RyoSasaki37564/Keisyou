using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public ItemData[] m_itemInventry = new ItemData[8];
    public Toryugu[] m_armsInventry = new Toryugu[2];
    public ItemData[] m_kichouhinInventry;

    public List<ItemData> m_shortCutItems = new List<ItemData>();
    public List<Toryugu> m_mainArms = new List<Toryugu>();

    public void TestInventryMake()
    {
        m_itemInventry[0] = new ItemData(0, "丸薬", 3, 99, "体力を少し回復する。", "どこでも手に入る、ありふれた丸薬。", 0, 100);
        m_itemInventry[1] = new ItemData(1, "飴玉", 3, 99, "集中を少し回復する。", "どこでも手に入る、ありふれた飴玉。", 0, 100);
        m_itemInventry[2] = new ItemData(2, "例のアレ", 99, 99, "アレをアレする。", "アレだよアレ。", 0, 99999);
        m_itemInventry[3] = new ItemData(3, "龍骨", 0, 99, "ほね。。", "ほねです。", 0, 500);
        m_itemInventry[4] = new ItemData(4, "龍油", 0, 99, "りゅーゆ。。", "くっさいんですわこれが。", 0, 1000);
        m_itemInventry[5] = new ItemData(5, "ショウグンムカデ", 0, 99, "百足さん", "きんめぇ。", 0, 50);
        m_itemInventry[6] = new ItemData(6, "ヤマガエル", 0, 99, "けろけろ。。", "がまです。", 0, 50);
        m_itemInventry[7] = new ItemData(7, "石ころ", 0, 99, "スとーーん", "いしです。", 0, 1);

        m_armsInventry[0] = new Toryugu(0, "自我", 50, 12, 0, 0, 0, 15, "じがっががっががが！");
        m_armsInventry[1] = new Toryugu(1, "処刑者", 85, 9, 0, 2, 1, 30, "おめーの首ねーから！");

        m_mainArms.Add(m_armsInventry[1]);
        m_mainArms.Add(m_armsInventry[0]);
    }

    public void IntoShortCut(int id)
    {
        if (!m_shortCutItems.Contains(m_itemInventry[id]))
        {
            m_shortCutItems.Add(m_itemInventry[id]);
        }
        else
        {
            Debug.Log("元からあんねん");
        }
    }

    public void RemovefromShortCut(int id)
    {
        if(m_shortCutItems.Contains(m_itemInventry[id]))
        {
            m_shortCutItems.Remove(m_itemInventry[id]);
        }
        else
        {
            Debug.Log("元からないねん");
        }
    }

    public void AddMainArm(Toryugu t)
    {
        if(m_mainArms.Count < 5)
        {
            m_mainArms.Add(t);
        }
        else
        {
            Debug.Log("これ以上持てない");
        }
    }

    public void RemoveMainArm(Toryugu t)
    {
        if(m_mainArms.Count > 2)
        {
            m_mainArms.Remove(t);
        }
        else
        {
            Debug.Log("一本はないとダメ");
        }
    }

    public void ReplaceMainArm(Toryugu prev, Toryugu iretai)
    {
        if(m_mainArms.Contains(iretai))
        {
            int nowP = 0;
            int nowI = 0;
            for(var i = 0; i < m_mainArms.Count; i++)
            {
                if(m_mainArms[i] == prev)
                {
                    nowP = i;
                }
                if(m_mainArms[i] == iretai)
                {
                    nowI = i;
                }
            }
            m_mainArms.Remove(prev);
            m_mainArms.Remove(iretai);
            m_mainArms.Insert(nowP, iretai);
            m_mainArms.Insert(nowI, prev);
        }
        else
        {
            int nowP = 0;
            for (var i = 0; i < m_mainArms.Count; i++)
            {
                if (m_mainArms[i] == prev)
                {
                    nowP = i;
                }
            }
            m_mainArms.Remove(prev);
            m_mainArms.Insert(nowP, iretai);
        }
    }
}

public class ItemData
{
    int m_id;
    string m_name;
    int m_count;
    int m_maxCount;
    string m_effectText;
    string m_fravorText;
    bool m_isKeyItem;
    int m_price;

    public ItemData(int id, string name, int count, int maxCount, string effect, string fravor, int isKey, int price)
    {
        m_id = id;
        m_name = name;
        m_count = count;
        m_maxCount = maxCount;
        m_effectText = effect;
        m_fravorText = fravor;
        if(isKey == 1) { m_isKeyItem = true; }
        m_price = price;
    }

    public int GetID { get => m_id; }
    public string GetName { get => m_name; }
    public int SetCount { set => m_count = value; }
    public int GetCount { get => m_count; }
    public int GetMaxCount { get => m_maxCount; }
    public string GetEffectText { get => m_effectText; }
    public string GetFravorText { get => m_fravorText; }
    public bool GetIsKeyItem { get => m_isKeyItem; }
    public int GetPrice { get => m_price; }

    /// <summary>
    /// アイテム入手時に呼ぶ。
    /// </summary>
    /// <param name="itemCount">手に入れたアイテムの、現状のインベントリ上のカウント先</param>
    /// <param name="gettingCount">今回手に入れた個数</param>
    /// <returns></returns>
    public int GetItem(int itemCount, int gettingCount)
    {
        itemCount += gettingCount;
        if(m_count > m_maxCount)
        {
            int buy = itemCount - m_maxCount;
            Buy(buy);
            itemCount = m_maxCount;
        }
        return itemCount;
    }

    public int Buy(int buyCount)
    {
        int num = 0;
        if(!m_isKeyItem)
        {
            if(buyCount <= m_count)
            {
                m_count -= buyCount;
                num = PlayerDataAlfa.Instance.m_money += (buyCount * m_price) / 2;
            }
        }
        return num;
    }
}

public class Toryugu
{
    int m_id;
    string m_name;
    bool m_isItHave;
    int m_atk;
    int m_dodge;
    int m_type;
    bool m_isSrust;
    int m_motionType;
    int m_ryugekiTekisei;
    string m_fravorText;

    public Toryugu(int id, string name, int atk, int dod, int type, int isSrust, int motion, int ryugeki, string fraText)
    {
        m_id = id;
        m_name = name;
        m_atk = atk;
        m_dodge = dod;
        m_type = type;
        if(isSrust > 0) { m_isSrust = true; }
        m_motionType = motion;
        m_ryugekiTekisei = ryugeki;
        m_fravorText = fraText;
    }
    public int GetID { get => m_id; }
    public string GetName { get => m_name; }
    public bool GetIsHave { get => m_isItHave; }
    public string GetFravorText { get => m_fravorText; }
    public int GetAtk { get => m_atk; }
    public int GetDod { get => m_dodge; }
    public int GetKatyouHugetu { get => m_type; }
    public bool GetIsSrust { get => m_isSrust; }
    public int GetMotion { get => m_motionType; }
    public int GetRyugekiTekisei { get => m_ryugekiTekisei; }

    public void InToTheHand()
    {
        m_isItHave = true;
    }
}