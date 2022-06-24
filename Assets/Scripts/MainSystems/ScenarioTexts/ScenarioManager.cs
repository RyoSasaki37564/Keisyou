using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ScenarioManager : MonoBehaviour
{
    static ScenarioManager m_instance;

    public static ScenarioManager Instance
    {
        get
        {
            if(m_instance == null)
            {
                GameObject go = new GameObject("ScenarioManager");
                m_instance = go.AddComponent<ScenarioManager>();
            }

            return m_instance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
             CustomPlayableBehaviour.m_director.playableGraph.GetRootPlayable(0).SetSpeed(1d);
        }
    }

    public void OnDisable()
    {
        if (m_instance)
        {
            Destroy(m_instance.gameObject);
        }
    }

    /// <summary>
    /// 非モノビクラスからコルーチンを呼ぶ機構
    /// </summary>
    /// <param name="cor"></param>
    /// <returns></returns>
    public static Coroutine PlayableBehaviourCoroutine(IEnumerator cor)
    {
        return Instance.StartCoroutine(cor);
    }
}
