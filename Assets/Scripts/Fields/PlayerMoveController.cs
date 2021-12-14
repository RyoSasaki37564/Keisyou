using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ターンベースでのプレイヤー移動を制御するコンポーネント
/// </summary>
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

        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        /*本来の移動処理。こちらは斜め移動を禁止する。
        if (v == 0)
        {
            h = Input.GetAxisRaw("Horizontal");
        }
        if(h == 0)
        {
            v = Input.GetAxisRaw("Vertical");
        }
        */
        if (h != 0 || v != 0)
        {
            if(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) //ダッシュ
            {
                m_gridMove.Move((int)h, (int)v, m_moveTime/m_dash);
            }
            else
            {
                m_gridMove.Move((int)h, (int)v, m_moveTime);
            }
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
