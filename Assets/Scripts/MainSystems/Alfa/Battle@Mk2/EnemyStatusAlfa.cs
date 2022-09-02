public class EnemyStatusAlfa
{
    public readonly int m_id; 
    public readonly string m_name;
    public readonly int m_hp;
    public readonly int m_stamina;
    public int m_nowStamina;
    public readonly int m_atk;
    public readonly int m_def;
    public readonly int m_kachouFugetsu;

    public EnemyStatusAlfa(int id, string name, int hp, int stm, int atk, int def, int kacho)
    {
        this.m_id = id;
        this.m_name = name;
        this.m_hp = hp;
        this.m_stamina = stm;
        this.m_nowStamina = m_stamina;
        this.m_atk = atk;
        this.m_def = def;
        this.m_kachouFugetsu = kacho;
    }
}
