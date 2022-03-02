using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NineKeyInputNomal : MonoBehaviour
{

    [SerializeField] GameObject m_effectSlash = default; //斬撃マーク
    GameObject[] m_slashs = new GameObject[9]; //斬撃のオブジェクトプール
    int m_slashIndexer = 0;

    [SerializeField] GameObject m_effectSrast = default; //刺突マーク
    GameObject[] m_srasts = new GameObject[9]; //刺突のオブジェクトプール
    int m_srastIndexer = 0;

    public struct CommandCode
    {
        public int Number { get; set; }
        public int Contact { get; set; }

        /// <summary>
        /// 入力されたキーの番号と接触方向を受け取り、ID情報に変換する
        /// </summary>
        /// <param name="numID">キー番号</param>
        /// <param name="conID">接触方向</param>
        public CommandCode(int numID, int conID)
        {
            Number = numID;
            Contact = conID;
        }
    }
    List<CommandCode> m_commandList = new List<CommandCode>(); //ここに格納された値を参照し、該当する龍撃の演出を呼び出す。



    Vector3 m_mousePosDelta;

    bool m_isIn = false;
    bool m_isInStopper = false;

    bool m_isStart = false;

    bool m_slustFlg = false;

    bool m_zangekiFlg = false;

    float m_zangekiDirection; //斬撃角度

    const float c_originDir = 22.5f; //斬撃方向IDをとるための原角度

    private void Awake()
    {
        //マーカーエフェクトを生成しプール
        for (var i = 0; i < 9; i++)
        {
            var x = Instantiate(m_effectSlash);
            m_slashs[i] = x;
            m_slashs[i].SetActive(false);
        }
        for (var i = 0; i < 9; i++)
        {
            var x = Instantiate(m_effectSrast);
            m_srasts[i] = x;
            m_srasts[i].SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //m_thisCol = this.gameObject.GetComponent<Collider2D>();

        TouchManager.Began += (info) =>
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if(hit.collider != null && hit.transform.tag == "NineKey") //(hit.collider == m_thisCol)
            {
                m_isIn = true;
                m_slustFlg = true;
            }
        };

        TouchManager.Moved += (info) =>
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null)
            {
                if(m_isInStopper == false)
                {
                    m_isIn = true;
                    m_isInStopper = true;
                }
            }
        };

        TouchManager.Ended += (info) =>
        {
            m_isIn = false;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null && m_slustFlg == true)//(hit.collider == m_thisCol && m_slustFlg == true)
            {
                Debug.Log(hit.collider.name + " " + 5);
            }
            else if (m_zangekiFlg == true)
            {
                Debug.Log(this.gameObject.name + " " + ZangekiDirection() + "うち");
            }
            m_slustFlg = false;

            m_zangekiFlg = false;

            m_isInStopper = false;

        };
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButton(0) && m_isIn == true)
        {
            m_isIn = false;
            m_mousePosDelta = Input.mousePosition;
            m_zangekiFlg = true;
        }
    }

    private void OnMouseExit()
    {
        if (m_zangekiFlg == true)
        {
            Debug.Log(this.gameObject.name + " " + ZangekiDirection() + "ぬけ");

            m_zangekiFlg = false;

            m_isInStopper = false;

        }
    }

    int ZangekiDirection()
    {
        var heading = Input.mousePosition - m_mousePosDelta;
        var distance = heading.magnitude;
        var direction = heading / distance;
        m_zangekiDirection = Mathf.Repeat(Mathf.Atan2(direction.y, direction.x) * (180 / Mathf.PI), 360);

        //22.5　は　45 の半分。45は　360 を斬撃の方向数「8」で割った数(5 は突きとして扱う)
        
        //ゲーム画面では 九字鍵は
        // 1 2 3
        // 4 5 6
        // 7 8 9
        //　と並んでいるが、この処理部分では円形角度計算を行うため少しでもif文の可読性を上げるために
        //　7,8,9,6,3,2,1,elseで4　という風にぐるりと円形イメージで記述を行う

        if (c_originDir <= m_zangekiDirection
            && m_zangekiDirection < c_originDir + 45) 
        {
            return 7;
        }
        else if(c_originDir + 45 <= m_zangekiDirection
            && m_zangekiDirection < c_originDir + 45 * 2)
        {
            return 8;
        }
        else if (c_originDir + 45 * 2 <= m_zangekiDirection
            && m_zangekiDirection < c_originDir + 45 * 3)
        {
            return 9;
        }
        else if (c_originDir + 45 * 3 <= m_zangekiDirection
            && m_zangekiDirection < c_originDir + 45 * 4)
        {
            return 6;
        }
        else if (c_originDir + 45 * 4 <= m_zangekiDirection
            && m_zangekiDirection < c_originDir + 45 * 5)
        {
            return 3;
        }
        else if (c_originDir + 45 * 5 <= m_zangekiDirection
            && m_zangekiDirection < c_originDir + 45 * 6)
        {
            return 2;
        }
        else if (c_originDir + 45 * 6 <= m_zangekiDirection
            && m_zangekiDirection < c_originDir + 45 * 7)
        {
            return 1;
        }
        else
        {
            return 4;
        }
    }
}
