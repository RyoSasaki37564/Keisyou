public class EnemyStatusAlfa
{
    int m_hp;
    int m_stamina;
    int m_atk;
    int m_def;
    int m_kachouFugetsu;
    string m_name;

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
