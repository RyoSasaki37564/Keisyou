using System.IO;
using UnityEngine;
using UnityEditor;

public class EditorSample : EditorWindow
{
    SOSample _sample;
    private const string ASSET_PATH = "Assets/Resources/SOSample.asset";

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

        Color defaultColor = GUI.backgroundColor;
        using (new GUILayout.VerticalScope(EditorStyles.helpBox))
        {
            GUI.backgroundColor = Color.gray;
            using (new GUILayout.HorizontalScope(EditorStyles.toolbar))
            {
                GUILayout.Label("設定");
            }
            GUI.backgroundColor = defaultColor;

            _sample.SampleIntValue = EditorGUILayout.IntField("サンプルint", _sample.SampleIntValue);
        }
        using (new GUILayout.VerticalScope(EditorStyles.helpBox))
        {
            GUI.backgroundColor = Color.gray;
            using (new GUILayout.HorizontalScope(EditorStyles.toolbar))
            {
                GUILayout.Label("ファイル操作");
            }
            GUI.backgroundColor = defaultColor;

            GUILayout.Label("パス：" + ASSET_PATH);

            using (new GUILayout.HorizontalScope(GUI.skin.box))
            {
                GUI.backgroundColor = Color.white;
                // 読み込みボタン
                if (GUILayout.Button("読み込み"))
                {
                    Import();
                }
                GUI.backgroundColor = Color.white;
                // 書き込みボタン
                if (GUILayout.Button("書き込み"))
                {
                    Export();
                }
                GUI.backgroundColor = defaultColor;
            }
        }
    }
    private void Export()
    {
        // 読み込み
        SOSample sample = AssetDatabase.LoadAssetAtPath<SOSample>(ASSET_PATH);
        if (sample == null)
        {
            sample = ScriptableObject.CreateInstance<SOSample>();
        }

        // 新規の場合は作成
        if (!AssetDatabase.Contains(sample as UnityEngine.Object))
        {
            string directory = Path.GetDirectoryName(ASSET_PATH);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            // アセット作成
            AssetDatabase.CreateAsset(sample, ASSET_PATH);
        }

        // コピー
        EditorUtility.CopySerialized(_sample, sample);

        // 直接編集できないようにする
        sample.hideFlags = HideFlags.NotEditable;
        // 更新通知
        EditorUtility.SetDirty(sample);
        // 保存
        AssetDatabase.SaveAssets();
        // エディタを最新の状態にする
        AssetDatabase.Refresh();
    }

    private void Import()
    {
        if (_sample == null)
        {
            _sample = ScriptableObject.CreateInstance<SOSample>();
        }

        SOSample sample = AssetDatabase.LoadAssetAtPath<SOSample>(ASSET_PATH);
        if (sample == null)
            return;

        // コピーする
        EditorUtility.CopySerialized(sample, _sample);
    }
}