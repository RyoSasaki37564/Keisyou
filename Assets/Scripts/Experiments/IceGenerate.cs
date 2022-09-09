using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceGenerate : MonoBehaviour
{
    [SerializeField] List<GameObject> m_iceList = new List<GameObject>();
    [SerializeField] GameObject m_flashFade;

    int id = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Iceicle();
    }

    private void OnEnable()
    {
        id = 0;
        Iceicle();
    }

    void Iceicle()
    {
        m_iceList.Clear();
        for (var i = 0; i < transform.childCount; i++)
        {
            m_iceList.Add(transform.GetChild(i).gameObject);
            transform.GetChild(i).gameObject.SetActive(false);
        }
        StartCoroutine(ICEBorn(0.08f));
    }

    IEnumerator ICEBorn(float time)
    {
        Debug.Log(id);
        yield return new WaitForSeconds(time);
        m_iceList[id].SetActive(true);
        id++;
        if(id != m_iceList.Count)
        {
            StartCoroutine(ICEBorn(time -= time / 10));
        }
        else
        {
            IEnumerator XYZ()
            {
                yield return new WaitForSeconds(1.2f);
                m_flashFade.SetActive(true);
                gameObject.SetActive(false);
            }
            StartCoroutine(XYZ());
        }
    }
}
