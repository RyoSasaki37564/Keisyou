﻿#if UNITY_EDITOR
#define IS_EDITOR
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TouchManager : MonoBehaviour
{
    //シングルトン
    public static TouchManager _instance;
    private static TouchManager Instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = new GameObject(typeof(TouchManager).Name);
                _instance = obj.AddComponent<TouchManager>();
            }
            return _instance;
        }
    }
    public TouchInfo _info = new TouchInfo();
    private event System.Action<TouchInfo> _begane;
    private event System.Action<TouchInfo> _moved;
    private event System.Action<TouchInfo> _ended;

    //タッチ開始のイベント
    public static event System.Action<TouchInfo> Began
    {
        add
        {
            Instance._begane += value;
        }
        remove
        {
            Instance._begane -= value;
        }
    }
    public static event System.Action<TouchInfo> Moved
    {
        add
        {
            Instance._moved += value;
        }
        remove
        {
            Instance._moved -= value;
        }
    }
    public static event System.Action<TouchInfo> Ended
    {
        add
        {
            Instance._ended += value;
        }
        remove
        {
            Instance._ended -= value;
        }
    }
    public TouchState State
    {
        get
        {
#if UNITY_STANDALONE || UNITY_WEBGL || IS_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                return TouchState.Began;
            }
            else if (Input.GetMouseButton(0))
            {
                return TouchState.Moved;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                return TouchState.Ended;
            }
#elif UNITY_ANDROID || UNITY_ANDROID_API

            foreach (Touch touch in Input.touches)
            {
                /*
                if (Input.touchCount > 0)
                {
                */
                switch (touch.phase)//switch (Input.GetTouch(0).phase)
                {
                        case TouchPhase.Began:
                        Debug.Log(touch.fingerId);
                            return TouchState.Began;
                        case TouchPhase.Moved:
                    case TouchPhase.Stationary:
                        return TouchState.Moved;
                        case TouchPhase.Canceled:
                        case TouchPhase.Ended:
                            return TouchState.Ended;
                        default:
                            break;
                    }
                //}
            }
#endif
            return TouchState.None;
        }
    }

    // タッチされてる位置
    private Vector2 Position
    {
        get
        {
#if UNITY_STANDALONE || UNITY_WEBGL || IS_EDITOR
            return State == TouchState.None ?
                Vector2.zero : (Vector2)Input.mousePosition;   //三項演算子
#elif UNITY_ANDROID || UNITY_ANDROID_API
            return Input.GetTouch(0).position;

#endif

        }
    }

    private void Update()
    {
    //    fingerCount = 0;
    //    foreach (Touch touch in Input.touches)
    //    {
            if (State == TouchState.Began)
            {
                _info.screenPoint = Position;
                _info.deltaScreenPoint = Vector2.zero;
                if (_begane != null)
                {
                    _begane(_info);
                }
            }
            else if (State == TouchState.Moved)
            {
                _info.deltaScreenPoint = Position - _info.screenPoint;
                _info.screenPoint = Position;
                if (_moved != null)
                {
                    _moved(_info);
                }
            }
            else if (State == TouchState.Ended)
            {
                _info.deltaScreenPoint = Position - _info.screenPoint;
                _info.screenPoint = Position;
                if (_ended != null)
                {
                    _ended(_info);
                }
            }
            else
            {
                _info.screenPoint = Vector2.zero;
                _info.deltaScreenPoint = Vector2.zero;
            }
            //fingerCount++;
        //}
    }

}

//タッチ情報
public class TouchInfo
{
    //タッチされたスクリーン座標
    public List<Vector2> screenPoints = new List<Vector2>();
    public Vector2 screenPoint;
    //１フレーム前のスクリーン座標との差分
    public Vector2 deltaScreenPoint;
    //タッチされたビューポート座標
    private Vector2 _viewPoint = Vector2.zero;
    public Vector2 ViewPoint
    {
        get
        {
            _viewPoint.x = screenPoint.x / Screen.width;
            _viewPoint.y = screenPoint.y / Screen.height;
            return _viewPoint;
        }
    }
    //１フレーム前のビューポート座標との差分
    private Vector2 _deltaViewPoint = Vector2.zero;
    public Vector2 DeltaViewPoint
    {
        get
        {
            _deltaViewPoint.x = deltaScreenPoint.x / Screen.width;
            _deltaViewPoint.y = deltaScreenPoint.y / Screen.width;
            return _deltaViewPoint;
        }
    }
}

//タッチ状態
public enum TouchState
{
    None = 0,
    Began = 1,
    Moved = 2,
    Ended = 3,

}
