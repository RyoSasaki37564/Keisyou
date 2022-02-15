using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Mushimizu : ItemBase
{
    [SerializeField] GameObject m_poisonObj = default;

    [SerializeField] SEPlay m_SE = default;

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
            var x = Instantiate(m_poisonObj);
            m_SE.MyPlayOneShot();
            x.SetActive(true);
        }
    }

    public void Kakunin()
    {
        base.Panneler(Player.Instance.m_itemMasterTable[2]._name,
                    Player.Instance.m_itemMasterTable[2]._InfoText, PannelingItemKarsol.musimizu);
    }
}
