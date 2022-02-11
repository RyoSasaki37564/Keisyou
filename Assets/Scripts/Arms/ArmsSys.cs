using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmsSys : MonoBehaviour
{
    [SerializeField] List<Sprite> m_armsSpriteMaster = new List<Sprite>();

    [SerializeField] Sprite[] m_typeSpriteMaster = new Sprite[4]; //属性アイコン
    [SerializeField] Image m_mainArmType = default; //装備中の属性

    [SerializeField] Image m_mainArm = default;
    [SerializeField] Image m_rightArm = default;
    [SerializeField] Image m_leftArm = default;

    public static int m_carsol = 0; //現在使用中の武器情報を指すID

    // Start is called before the first frame update
    void Start()
    {
        ArmsCicleDrow();
    }

    public void ArmsCycleR()
    {
        m_carsol++;
        ArmsCicleDrow();
    }
    public void ArmsCycleL()
    {
        if(m_carsol != 0)
        {
            m_carsol--;
        }
        else
        {
            m_carsol = m_armsSpriteMaster.Count - 1;
        }
        ArmsCicleDrow();
    }

    void ArmsCicleDrow()
    {
        m_carsol = m_carsol % m_armsSpriteMaster.Count;
        m_mainArm.sprite = m_armsSpriteMaster[m_carsol];
        if (m_carsol == 0)
        {
            m_rightArm.sprite = m_armsSpriteMaster[m_carsol + 1];
            m_leftArm.sprite = m_armsSpriteMaster[m_armsSpriteMaster.Count - 1];
        }
        else if (m_carsol == m_armsSpriteMaster.Count - 1)
        {
            m_rightArm.sprite = m_armsSpriteMaster[0];
            m_leftArm.sprite = m_armsSpriteMaster[m_carsol - 1];
        }
        else
        {
            m_rightArm.sprite = m_armsSpriteMaster[m_carsol + 1];
            m_leftArm.sprite = m_armsSpriteMaster[m_carsol - 1];
        }
        m_mainArmType.sprite = m_typeSpriteMaster[Player.Instance.m_armsMasterTable[m_carsol]._type];
    }
}
