﻿using System.Collections;
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

        if (h != 0 || v != 0)
        {
            m_gridMove.Move((int)h, (int)v, m_moveTime);
        }


    }
    private void LateUpdate()
    {
        if (!m_gridMove.IsMoving) //入力がなければアイドルする
        {
            m_walkingAnim.SetBool("SetIdle", true);
        }
        else if (m_walkingAnim) //入力に応じて歩行アニメーション
        {
            m_walkingAnim.SetBool("SetIdle", false);
            m_walkingAnim.SetFloat("SetWalkH", h);
            m_walkingAnim.SetFloat("SetWalkV", v);
        }
    }
}