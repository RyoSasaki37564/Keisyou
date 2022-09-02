
public class FuzzyLogic
{
    /// <summary>
    /// 右肩上がり、傾斜が正のメンバーシップ関数
    /// </summary>
    /// <param name="value">target value</param>
    /// <param name="x0">left border</param>
    /// <param name="x1">top point</param>
    /// <returns></returns>
    public static float FuzzyGrade(float value, float x0, float x1)
    {
        float result;
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

    /// <summary>
    /// 右肩下がり、傾斜が逆のメンバーシップ関数
    /// </summary>
    /// <param name="value">target value</param>
    /// <param name="x0">top point</param>
    /// <param name="x1">right border</param>
    /// <returns></returns>
    public static float FuzzyReverseGrade(float value, float x0, float x1)
    {
        float result;
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


    /// <summary>
    /// 三角形のメンバーシップ関数
    /// </summary>
    /// <param name="value">target value</param>
    /// <param name="x0">left border</param>
    /// <param name="x1">top point</param>
    /// <param name="x2">right border</param>
    /// <returns></returns>
    public static float FuzzyTriangle(float value, float x0, float x1, float x2)
    {
        float result;
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

    /// <summary>
    /// 台形のメンバーシップ関数
    /// </summary>
    /// <param name="value">target value</param>
    /// <param name="x0">left border</param>
    /// <param name="x1">left top</param>
    /// <param name="x2">right top</param>
    /// <param name="x3">right border</param>
    /// <returns></returns>
    public static float FuzzyTrapezoid(float value, float x0, float x1, float x2, float x3)
    {
        float result;
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
