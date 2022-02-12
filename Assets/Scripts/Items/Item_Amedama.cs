using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Amedama : ItemBase
{
    [SerializeField] int m_sweets = 20;

    public override void Start()
    {
        base.Start();
        m_intCount = Player.Instance.m_itemMasterTable[0]._count;
    }

    public override void UseItem()
    {
        if (this.m_canUse == true)
        {
            base.UseItem();
            Player.Instance.Relax(m_sweets);
        }
    }

    public void Kakunin()
    {
        base.Panneler(Player.Instance.m_itemMasterTable[1]._name,
                    Player.Instance.m_itemMasterTable[1]._InfoText, PannelingItemKarsol.amedama);
    }
}
