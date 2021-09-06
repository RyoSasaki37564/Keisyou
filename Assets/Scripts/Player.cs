using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //プレイヤー体力バー
    [SerializeField]
    Slider m_playerHpSlider = default;
    //プレイヤー体力最大値
    [SerializeField]
    int m_playerMaxHp = 100;
    //プレイヤー現在体力
    public int m_playerCurrentHp;

    // Start is called before the first frame update
    void Start()
    {
        m_playerCurrentHp = m_playerMaxHp / 10;
        m_playerHpSlider = GameObject.Find("PHPSlider").GetComponent<Slider>();
        m_playerHpSlider.maxValue = m_playerMaxHp;
        m_playerHpSlider.value = m_playerCurrentHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jikokanri(int iyasi)//ひっさつ２
    {
        if (m_playerCurrentHp < m_playerMaxHp)
        {
            m_playerCurrentHp += iyasi;
            m_playerHpSlider.value = m_playerCurrentHp;
            Debug.Log("回復しますた");
        }
    }
}
