using UnityEngine;

public class ScriptableObjectSampleLoader : MonoBehaviour
{
    private SOSample _sample;

    private void Start()
    {
        // 読み込み & 生成（ファイル名はASSET_PATHで定義したもの）
        _sample = ScriptableObject.Instantiate(Resources.Load<SOSample>("SOtSample"));
    }
}