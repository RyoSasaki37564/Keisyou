using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEPersonalManager : QTEPatterner
{
    [SerializeField] float m_xBorder = 1.0f;
    [SerializeField] float m_xLimit = 2.5f;

    [SerializeField] float m_yBorder = 1.0f;
    [SerializeField] float m_yLimit = 2.5f;

    [SerializeField] GameObject m_frame = default; // 限界規格

    public override IEnumerator QTERoader()
    {
        if(m_indexer == 0)
        {
            m_QTEs.Insert(0, this.gameObject);
            m_indexer++;
        }

        Vector2 pos = new Vector2();

        do
        {
            pos = RePositionning(pos);
        }
        while (!(pos.x < m_frame.transform.position.x && pos.x > -m_frame.transform.position.x
                && pos.y < m_frame.transform.position.y && pos.y > -m_frame.transform.position.y));

        m_QTEs[m_indexer].transform.position = pos;
        return base.QTERoader();
    }

    Vector2 RePositionning(Vector2 position)
    {
        int intRand = Random.Range(0, 4);

        float x = Random.Range(m_xBorder, m_xLimit);
        float y = Random.Range(m_yBorder, m_yLimit);

        switch (intRand)
        {
            case 0:
                //右上スタイル
                position = new Vector2(m_QTEs[m_indexer - 1].transform.position.x + x, m_QTEs[m_indexer - 1].transform.position.y + y);
                break;
            case 1:
                //右下スタイル
                position = new Vector2(m_QTEs[m_indexer - 1].transform.position.x + x, m_QTEs[m_indexer - 1].transform.position.y - y);
                break;
            case 2:
                //左下スタイル
                position = new Vector2(m_QTEs[m_indexer - 1].transform.position.x - x, m_QTEs[m_indexer - 1].transform.position.y - y);
                break;
            case 3:
                //左上スタイル
                position = new Vector2(m_QTEs[m_indexer - 1].transform.position.x - x, m_QTEs[m_indexer - 1].transform.position.y + y);
                break;
        }

            return position;
    }
}
