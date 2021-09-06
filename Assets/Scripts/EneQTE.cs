using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EneQTE : MonoBehaviour
{
    int m_QTERandom = 0; //QTE発生判定用乱数

    [SerializeField] float m_waitingQTE = 5.0f; //QTE判定待ち時間

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator QTESet()
    {
        yield return new WaitForSeconds(m_waitingQTE);

    }
}
