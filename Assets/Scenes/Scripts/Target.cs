using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int m_tergetNum { get; set; } //標的の要素番号を格納

    // Start is called before the first frame update
    void Start()
    {
        m_tergetNum = 0;
    }
}
