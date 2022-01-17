using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NineKeyInput : MonoBehaviour
{
    [SerializeField] GameObject[] m_nines = new GameObject[9];

    Vector3 m_mousePosDelta;

    public struct CommandCode
    {
        public int number { get; set; }
        public int contact { get; set; }

        public CommandCode(int numID, int conID)
        {
            number = numID;
            contact = conID;
        }
    }
    List<CommandCode> m_commandList = new List<CommandCode>();
    int m_indexer = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_indexer = 0;
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider)
            {
                Debug.Log(GetAim(m_mousePosDelta, Input.mousePosition));

                Debug.Log("接触情報：" + hit.collider.gameObject.transform.parent.name + " " + hit.collider.gameObject.name);

                CommandCode m_CC = new CommandCode(int.Parse(hit.collider.gameObject.transform.parent.name), 
                    int.Parse(hit.collider.gameObject.name));
                hit.collider.gameObject.transform.parent.gameObject.SetActive(false);
                m_commandList.Add(m_CC);
            }
        }
    }

    private void FixedUpdate()
    {
        m_mousePosDelta = Input.mousePosition;
    }

    public float GetAim(Vector3 p1, Vector3 p2)
    {
        float dx = p2.x - p1.x;
        float dy = p2.y - p1.y;
        float rad = Mathf.Atan2(dy, dx);
        return rad * Mathf.Rad2Deg;
    }
}