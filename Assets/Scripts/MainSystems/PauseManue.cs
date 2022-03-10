using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManue : MonoBehaviour
{
    [SerializeField] List<GameObject> m_ryugekiMaster = new List<GameObject>();

    int m_carsol = 0;

    // Start is called before the first frame update
    void Start()
    {
        ArmsCicleDrow();
    }

    public void ViewR()
    {
        m_carsol++;
        ArmsCicleDrow();
    }
    public void ViewL()
    {
        if (m_carsol != 0)
        {
            m_carsol--;
        }
        else
        {
            m_carsol = m_ryugekiMaster.Count - 1;
        }
        ArmsCicleDrow();
    }

    void ArmsCicleDrow()
    {
        m_carsol = m_carsol % m_ryugekiMaster.Count;
        foreach(var i in m_ryugekiMaster)
        {
            i.SetActive(false);
        }
        m_ryugekiMaster[m_carsol].SetActive(true);
    }
}
