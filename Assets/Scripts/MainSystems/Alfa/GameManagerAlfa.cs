using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Title,
    Shuraku,
    Hunt,
    Battle,
}

public class GameManagerAlfa : MonoBehaviour
{
    static GameManagerAlfa m_instance;
    public static GameManagerAlfa Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = (GameManagerAlfa)FindObjectOfType(typeof(GameManagerAlfa));
                if (m_instance == null)
                {
                    Debug.LogError("インスタンス消失");
                }
            }
            return m_instance;
        }
    }

    public GameState m_nowState = GameState.Title;

    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
