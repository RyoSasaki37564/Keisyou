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

        //Task t = PreviousSceneOffAsync();
    }

    async Task PreviousSceneOffAsync()
    {
        await Task.Run(() =>
        {
            ObjectsOff();
        });

        Debug.Log("ロード完了");
        SceneManager.LoadScene("Battle", LoadSceneMode.Additive);
    }

    void ObjectsOff()
    {
        m_activateTargetList.Clear();
        GameObject[] fieldOnActives = Resources.FindObjectsOfTypeAll<GameObject>();
        m_activateTargetList = fieldOnActives.Where(o => o.activeSelf).ToList();
        m_activateTargetList.ForEach(o => o.SetActive(false));
    }

    public void ObjectsOn()
    {
        Task t = ObjsOnAsync();
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
