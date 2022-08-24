using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Destinasion
{
    Nazuchi_Miyama,
}

public class GoToField : MonoBehaviour
{
    [SerializeField] Destinasion m_destinasion = Destinasion.Nazuchi_Miyama;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerAvater")
        {
            GoToBattleField();
        }
    }

    void GoToBattleField()
    {
        switch (m_destinasion)
        {
            case Destinasion.Nazuchi_Miyama:
                SceneManager.LoadScene("FieldTest");
                break;
        }
    }
}
