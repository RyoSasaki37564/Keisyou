
public abstract class EnemyBattleAIBase
{
    /// <summary>
    /// 行動変化フラグ
    /// </summary>
    protected bool m_isMorphChanged;

    public abstract void EnemyActionSelect(int nowHP, int maxHP, ref int nowStamina, int maxStamina, int freePara0, int freePara0Max);
}