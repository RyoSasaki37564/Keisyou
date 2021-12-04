using UnityEngine;
using UnityEditor;

public class EditorSample : EditorWindow
{

    SOSample _sample;

    [MenuItem("Editor/Sample")]

    private static void Create()
    {
        // 生成
        GetWindow<EditorSample>("サンプル");
    }

    private void OnGUI()
    {
        if (_sample == null)
        {
            _sample = ScriptableObject.CreateInstance<SOSample>();
        }

        using (new GUILayout.HorizontalScope())
        {
            _sample.SampleIntValue = EditorGUILayout.IntField("サンプルint", _sample.SampleIntValue);
        }
    }
}