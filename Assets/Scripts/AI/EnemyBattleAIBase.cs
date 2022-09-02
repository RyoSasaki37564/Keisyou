
public abstract class EnemyBattleAIBase
{
    /// <summary>
    /// 行動変化フラグ
    /// </summary>
    protected bool m_isMorphChanged;


    /// <summary>
    /// 戻り値を使ってアニメーションの選択を行う
    /// </summary>
    /// <param name="nowHP"></param>
    /// <param name="maxHP"></param>
    /// <param name="nowStamina"></param>
    /// <param name="maxStamina"></param>
    /// <param name="freePara0"></param>
    /// <param name="freePara0Max"></param>
    /// <returns></returns>
    public abstract string EnemyActionSelect(int nowHP, int maxHP, ref int nowStamina, int maxStamina, int freePara0, int freePara0Max);
}