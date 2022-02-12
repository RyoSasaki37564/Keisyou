using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Gannyaku : ItemBase
{
    public override void Start()
    {
        base.Start();
        m_intCount = Player.Instance.m_itemMasterTable[0]._count;
    }

    public override void UseItem()
    {
        if(this.m_canUse == true)
        {
            base.UseItem();
            Player.Instance.Healing(Player.Instance.m_maxHP * 0.15f);
        }
    }

    public void Kakunin()
    {
        base.Panneler(Player.Instance.m_itemMasterTable[0]._name,
                    Player.Instance.m_itemMasterTable[0]._InfoText, PannelingItemKarsol.gannyaku);
    }
}
