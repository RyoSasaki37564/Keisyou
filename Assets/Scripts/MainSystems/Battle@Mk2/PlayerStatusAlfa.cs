public class PlayerStatusAlfa
{
    public readonly int m_level;
    public readonly int m_hp;
    public readonly int m_con;
    public readonly int m_dodge;
    public readonly int m_atk;
    public readonly int m_def;
    public readonly int m_kakugo;

    public PlayerStatusAlfa(int level, int hp, int con, int dod, int atk, int def, int kak)
    {
        this.m_level = level;
        this.m_hp = hp;
        this.m_con = con;
        this.m_dodge = dod;
        this.m_atk = atk;
        this.m_def = def;
        this.m_kakugo = kak;
    }
}