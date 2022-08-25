using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridMoveController))]
public class PlayerMoveController : MonoBehaviour
{
    /// <summary>１ターンで動くのにかける時間（単位: 秒）</summary>
    [SerializeField] float m_moveTime = 1f;
    /// <summary>ダッシュ時の加速力</summary>
    [SerializeField] float m_dash = 2.5f;
    GridMoveController m_gridMove = null;

    Animator m_walkingAnim = default; //歩行アニメ

    float h; // 横
    float v; // 縦

    public StandbyDirection m_dire = StandbyDirection.down;
    [SerializeField] BoxCollider2D m_eventSerchTrigger;

    void Start()
    {
        m_gridMove = GetComponent<GridMoveController>();
        m_walkingAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if (m_gridMove.IsMoving)    // 動いている最中は何もしない
        {
            return;
        }

        //斜めに動ける入力受付
        /*
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        /*本来の移動処理。こちらは斜め移動を禁止する。
        */
        if (v == 0)
        {
            h = Input.GetAxisRaw("Horizontal");
            if(h < 0)
            {
                m_dire = StandbyDirection.left;
            }
            else if(h > 0)
            {
                m_dire = StandbyDirection.right;
            }
        }
        if(h == 0)
        {
            v = Input.GetAxisRaw("Vertical");
            if(v < 0)
            {
                m_dire = StandbyDirection.down;
            }
            else if(v > 0)
            {
                m_dire = StandbyDirection.up;
            }
        }

        switch(m_dire)
        {
            case StandbyDirection.up:
                m_eventSerchTrigger.offset = new Vector2(0f, 1f);
                break;
            case StandbyDirection.right:
                m_eventSerchTrigger.offset = new Vector2(1f, 0f);
                break;
            case StandbyDirection.left:
                m_eventSerchTrigger.offset = new Vector2(-1f, 0f);
                break;
            case StandbyDirection.down:
                m_eventSerchTrigger.offset = new Vector2(0f, -1f);
                break;
        }

        if (h != 0 || v != 0)
        {
            if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) //ダッシュ
            {
                m_gridMove.Move((int)h, (int)v, m_moveTime/m_dash);
            }
            else
            {
                m_gridMove.Move((int)h, (int)v, m_moveTime);
            }
        }
        if(!ScenarioManager.Instance.m_scenarioFlg && Input.GetKeyDown(KeyCode.Q))
        {
            FieldEventManager.Instance.EventLoad(this.gameObject);
        }
    }
    private void LateUpdate()
    {
        //歩行アニメ処理
        m_walkingAnim.SetFloat("SetWalkH", h);
        m_walkingAnim.SetFloat("SetWalkV", v);
        if (!m_gridMove.IsMoving)
        {
            m_walkingAnim.SetBool("SetIdle", true);
        }
        else
        {
            m_walkingAnim.SetBool("SetIdle", false);
        }
    }
}
