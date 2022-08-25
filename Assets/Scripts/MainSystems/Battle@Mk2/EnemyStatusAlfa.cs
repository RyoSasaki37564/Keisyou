public class EnemyStatusAlfa
{
    public readonly int m_hp;
    public readonly int m_stamina;
    public readonly int m_atk;
    public readonly int m_def;
    public readonly int m_kachouFugetsu;
    public readonly string m_name;

    public EnemyStatusAlfa(int hp, int stm, int atk, int def, int kacho, string name)
    {
        this.m_hp = hp;
        this.m_stamina = stm;
        this.m_atk = atk;
        this.m_def = def;
        this.m_kachouFugetsu = kacho;
        this.m_name = name;
    }
}
