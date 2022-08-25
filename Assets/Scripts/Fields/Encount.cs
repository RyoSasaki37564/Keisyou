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

    [SerializeField] GameObject m_fade; //フェーダー

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
}
