using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Tilemaps;

[System.Serializable]
public class NPCMoveCustomPlayableBehaviour : PlayableBehaviour
{
    public static PlayableDirector m_director;
    [System.NonSerialized] public GameObject m_templateGameObject;

    //[SerializeField, Tooltip("速度倍率")] float m_speedRate = 3f;

    /// <summary>１ターンで動くのにかける時間（単位: 秒）</summary>
    float m_moveTime = 1f;

    GridMoveController m_gridMove;

    Animator m_walkingAnim = default; //歩行アニメ

    [SerializeField] float h; // 横
    [SerializeField] float v; // 縦

    [SerializeField] int m_goalX; // 目的地座標X
    [SerializeField] int m_goalY; // 目的地座標Y

    [SerializeField] GameObject m_charactor;
    GameObject m_thisGO;

    [SerializeField] Vector3 m_genePos;

    public override void OnPlayableCreate(Playable playable)
    {
        if (m_director == null)
        {
            m_director = playable.GetGraph().GetResolver() as PlayableDirector;
        }
    }

    //タイムライン開始時
    public override void OnGraphStart(Playable playable)
    {

    }

    //	タイムライン停止時
    public override void OnGraphStop(Playable playable)
    {

    }

    //このPlayableTrack再生時
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        if (!m_thisGO)
        {
            m_thisGO = GameObject.Instantiate(m_charactor, m_genePos, Quaternion.identity);
            m_gridMove = m_thisGO.GetComponent<GridMoveController>();
            m_walkingAnim = m_thisGO.GetComponent<Animator>();
        }
    }

    //	PlayableTrack停止時
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {

    }

    /// <summary>
    /// PlayableTrack再生時毎フレーム
    /// </summary>
    public override void PrepareFrame(Playable playable, FrameData info)
    {
        if (m_gridMove.IsMoving)    // 動いている最中は何もしない
        {
            return;
        }

        m_gridMove.Move((int)h, (int)v, m_moveTime);


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
