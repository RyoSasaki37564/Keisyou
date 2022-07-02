using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzyTest : MonoBehaviour
{
    [SerializeField] GameObject[] m_blocks = new GameObject[4];
    //[SerializeField] GameObject m_OtherCharactor;



    private void Start()
    {
        Move();
    }

    IEnumerator MoveOnOneS()
    {
        yield return new WaitForSeconds(1f);
        Move();
    }

    void Move()
    {


        StartCoroutine(MoveOnOneS());
    }

    // 傾斜型のメンバーシップ関数
    public float FuzzyGrade(float value, float x0, float x1)
    {
        float result = 0;
        if (value <= x0)
        {
            result = 0;
        }
        else if (value >= x1)
        {
            result = 1;
        }
        else
        {
            result = (value / (x1 - x0)) - (x0 / (x1 - x0));
        }
        return result;
    }

    // 傾斜が逆のメンバーシップ関数
    public float FuzzyReverseGrade(float value, float x0, float x1)
    {
        float result = 0;
        if (value <= x0)
        {
            result = 1;
        }
        else if (value >= x1)
        {
            result = 0;
        }
        else
        {
            result = (x1 / (x1 - x0)) - (value / (x1 - x0));
        }
        return result;
    }

    // 三角形のメンバーシップ関数
    public float FuzzyTriangle(float value, float x0, float x1, float x2)
    {
        float result = 0;
        if (value <= x0 || value >= x2)
        {
            result = 0;
        }
        else if (value == x1)
        {
            result = 1;
        }
        else if ((value > x0) && (value < x1))
        {
            result = (value / (x1 - x0)) - (x0 / (x1 - x0));
        }
        else
        {
            result = (x2 / (x2 - x1)) - (value / (x2 - x1));
        }
        return result;
    }

    // 台形のメンバーシップ関数
    public float FuzzyTrapezoid(float value, float x0, float x1, float x2, float x3)
    {
        float result = 0;
        if (value <= x0 || value >= x3)
        {
            result = 0;
        }
        else if ((value >= x1) && (value <= x2))
        {
            result = 1;
        }
        else if ((value > x0) && (value < x1))
        {
            result = (value / (x1 - x0)) - (x0 / (x1 - x0));
        }
        else
        {
            result = (x3 / (x3 - x2)) - (value / (x3 - x2));
        }
        return result;
    }
}
