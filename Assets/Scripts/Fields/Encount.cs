using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Threading.Tasks;
using System.Linq;

public class Encount : MonoBehaviour
{
    List<GameObject> m_activateTargetList = new List<GameObject>();

    //[SerializeField] GameObject m_fade; //フェーダー

    public void EnemyEncount()
    {
        ObjectsOff();
        SceneManager.LoadScene("BattleAlfa", LoadSceneMode.Additive);

        //StartCoroutine(ObjectsOff());
        //SceneManager.LoadScene("BattleAlfa", LoadSceneMode.Additive);

        //Task t = PreviousSceneOffAsync();
        //Debug.Log("非同期処理開始");
        //SceneManager.LoadScene("BattleAlfa", LoadSceneMode.Additive);
        //t.Wait();
    }

    async Task PreviousSceneOffAsync()
    {
        await Task.Run(() =>
        {
            ObjectsOff();
            Debug.Log("ロード完了");
        });

    }

    void ObjectsOff()
    {
        m_activateTargetList.Clear();
        GameObject[] fieldOnActives = GameObject.FindObjectsOfType<GameObject>(); // Resources.FindObjectsOfTypeAll<GameObject>();        
        m_activateTargetList = fieldOnActives.Where(o => o.activeSelf && o != this.gameObject 
        && o.gameObject.transform.root != this.gameObject.transform).ToList();
        foreach (var o in m_activateTargetList)
        {
            o.SetActive(false);
        }
    }

    public void ObjectsOn()
    {
        for (var i = 0; i < m_activateTargetList.Count; i++)
        {
            m_activateTargetList[i].SetActive(true);
        }
        //Task t = ObjsOnAsync();
    }

    async Task ObjsOnAsync()
    {
        await Task.Run(() =>
        {
            for(var i = 0; i < m_activateTargetList.Count; i++)
            {
                m_activateTargetList[i].SetActive(true);
            }
        });
    }
}
