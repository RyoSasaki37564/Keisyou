using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class TagSearcher : EditorWindow
{
    /// <summary> 検出したいtag名 </summary>
    [SerializeField] string m_searchingTagsName;

    /// <summary> プロジェクト内のオブジェクトすべて </summary>
    public GameObject[] m_obj;

    List<GameObject> m_viewObj = new List<GameObject>();

    [MenuItem("Editor/TagSearcher")]
    static void Create() //窓生成
    {
        GetWindow<TagSearcher>("TagSearcher");
    }

    public string SearchingTagsName
    {
        get { return m_searchingTagsName; }

#if UNITY_EDITOR
        set { m_searchingTagsName = value; }
#endif
    }

    private void OnGUI()
    {
        using (new GUILayout.VerticalScope(EditorStyles.helpBox))
        {
            GUI.backgroundColor = Color.gray;
            using (new GUILayout.HorizontalScope(EditorStyles.toolbar))
            {
                GUILayout.Label("SearchTag");
            }
            GUI.backgroundColor = Color.gray;
            SearchingTagsName = EditorGUILayout.TextField("tag", SearchingTagsName);
            Search(SearchingTagsName);
            /*
             ここにSearchの結果のオブジェクトを表示したい
             */
        }
    }

    List<GameObject> Search(string s)
    {
        m_obj = Resources.FindObjectsOfTypeAll<GameObject>();

        List<GameObject> m_goList = new List<GameObject>();

#if UNITY_EDITOR
        foreach(var i in m_obj)
        {
            if(i.tag == s)
            {
                m_goList.Add(i);
                Debug.Log(i.name);
            }
        }
        return m_goList;
#endif
    }
}
