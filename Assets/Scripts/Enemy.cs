using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //敵体力バー
    [SerializeField]
    Slider m_enemyHpSlider = default; 
    //敵体力最大値
    [SerializeField]
    int m_enemyMaxHp = 1000; 
    //敵現在体力
    public int m_enemyCurrentHp; 
    //敵の攻撃力
    [SerializeField]
    int m_enemyAttackPower = 100;
    //被弾判定
    public bool m_enemyDamageFlag = false; 


    // Start is called before the first frame update
    void Start()
    {
        //体力初期化
        m_enemyCurrentHp = m_enemyMaxHp;
        m_enemyHpSlider = GameObject.Find("EnemyHPSlider").GetComponent<Slider>();
        m_enemyHpSlider.maxValue = m_enemyMaxHp;
        m_enemyHpSlider.value = m_enemyCurrentHp;
    }

    // Update is called once per frame
    void Update()
    {
        //被ダメージ
        if(m_enemyDamageFlag)
        {
            Takendamage();
        }
    }

    void Takendamage()
    {
        m_enemyHpSlider.value = m_enemyCurrentHp;
        m_enemyDamageFlag = false;
    }

    public void YabaiTaken(int buttigiri)//ひっさつ１
    {
        m_enemyCurrentHp -= buttigiri;
        Takendamage();
    }
}
