using UnityEngine;

[System.Serializable]
public abstract class QuestBase : MonoBehaviour
{
    [SerializeField] public string m_name;

    [SerializeField, TextArea(1, 3)] protected string m_setumei;

    protected bool[] m_shinkoudoFlgs;

    protected int m_counter = 0; //クエスト進行度

    [System.NonSerialized] public bool m_endFlg;

    protected QuestBase(string name, string setumei, int flgCount)
    {
        m_name = name;
        m_setumei = setumei;
        m_shinkoudoFlgs = new bool[flgCount];
    }
    public abstract void QStateAdvance();

    public void QuestEnd()
    {
        m_endFlg = true;
    }
}