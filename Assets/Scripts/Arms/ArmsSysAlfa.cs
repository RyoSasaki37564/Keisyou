using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmsSysAlfa : MonoBehaviour
{
    [SerializeField] List<Sprite> m_armsSpriteMaster = new List<Sprite>();
    List<Sprite> m_mainArmsSpriteList = new List<Sprite>();

    [SerializeField] Sprite[] m_typeSpriteMaster = new Sprite[4]; //属性アイコン
    [SerializeField] Image m_mainArmType = default; //装備中の属性

    [SerializeField] Image m_mainArm = default;
    [SerializeField] Image m_rightArm = default;
    [SerializeField] Image m_leftArm = default;

    public static int m_carsol = 0;

    // Start is called before the first frame update
    void Start()
    {
        MainArmsSetting();
    }

    public void MainArmsSetting()
    {
        m_mainArmsSpriteList.Clear();
        for(var i = 0; i < PlayerDataAlfa.Instance.m_testInventry.m_mainArms.Count; i++)
        {
            m_mainArmsSpriteList.Add(m_armsSpriteMaster[PlayerDataAlfa.Instance.m_testInventry.m_mainArms[i].GetID]);
        }
        m_carsol = 0;
        ArmsCicleDrow();
    }

    public void ArmsCycleR()
    {
        m_carsol++;
        ArmsCicleDrow();
    }

    public void ArmsCycleL()
    {
        if (m_carsol != 0)
        {
            m_carsol--;
        }
        else
        {
            m_carsol = m_mainArmsSpriteList.Count - 1;
        }
        ArmsCicleDrow();
    }

    void ArmsCicleDrow()
    {
        m_carsol = m_carsol % m_mainArmsSpriteList.Count;
        m_mainArm.sprite = m_mainArmsSpriteList[m_carsol];
        if (m_carsol == 0)
        {
            m_rightArm.sprite = m_mainArmsSpriteList[m_carsol + 1];
            m_leftArm.sprite = m_mainArmsSpriteList[m_mainArmsSpriteList.Count - 1];
        }
        else if (m_carsol == m_mainArmsSpriteList.Count - 1)
        {
            m_rightArm.sprite = m_mainArmsSpriteList[0];
            m_leftArm.sprite = m_mainArmsSpriteList[m_carsol - 1];
        }
        else
        {
            m_rightArm.sprite = m_mainArmsSpriteList[m_carsol + 1];
            m_leftArm.sprite = m_mainArmsSpriteList[m_carsol - 1];
        }
        m_mainArmType.sprite = m_typeSpriteMaster[PlayerDataAlfa.Instance.m_testInventry.m_mainArms[m_carsol].GetKatyouHugetu];
    }
}
