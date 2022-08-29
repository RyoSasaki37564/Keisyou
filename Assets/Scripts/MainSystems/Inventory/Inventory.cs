using System.Collections;
using System.Collections.Generic;

public class Inventory
{
    public ItemData[] m_itemInventry = new ItemData[4];

    public void TestInventryMake()
    {
        m_itemInventry[0] = new ItemData(0, "丸薬", 3, 99, "体力を少し回復する。", "どこでも手に入る、ありふれた丸薬。", 0, 100);
        m_itemInventry[1] = new ItemData(1, "飴玉", 3, 99, "集中を少し回復する。", "どこでも手に入る、ありふれた飴玉。", 0, 100);
        m_itemInventry[2] = new ItemData(2, "例のアレ", 99, 99, "アレをアレする。", "アレだよアレ。", 0, 99999);
        m_itemInventry[3] = new ItemData(3, "左腕", 0, 1, "うで。。", "うでです。", 1, 1);
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
    public int GetCount { get => m_count; }
    public int GetMaxCount { get => m_maxCount; }
    public string GetEffectText { get => m_effectText; }
    public string GetFravorText { get => m_fravorText; }
    public bool GetIsKeyItem { get => m_isKeyItem; }
    public int GetPrice { get => m_price; }

    public int GetItem(int getCount, int saifu)
    {
        int num = m_count += getCount;
        if(num > m_maxCount)
        {
            int buy = num - m_maxCount;
            Buy(buy, saifu);
            m_count = m_maxCount;
        }
        return num;
    }

    public int Buy(int buyCount, int saifu)
    {
        int num = 0;
        if(!m_isKeyItem)
        {
            if(buyCount <= m_count)
            {
                m_count -= buyCount;
                num = saifu += (buyCount * m_price) / 2;
            }
        }
        return num;
    }
}