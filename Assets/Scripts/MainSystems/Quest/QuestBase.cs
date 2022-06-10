
public abstract class QuestBase
{
    protected string m_name;

    protected string m_setumei;

    protected bool[] m_shinkoudoFlgs;

    protected int m_counter = 0; //クエスト進行度

    public bool m_endFlg;

    protected QuestBase(string name, string setumei, int flgCount)
    {
        m_name = name;
        m_setumei = setumei;
        m_shinkoudoFlgs = new bool[flgCount];
    }

    public abstract void QStateAdvance();
}
