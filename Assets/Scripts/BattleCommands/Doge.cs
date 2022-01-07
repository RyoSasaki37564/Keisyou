using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doge : MonoBehaviour
{


    public void DogeAct()
    {

        if (BattleManager._theTurn == BattleManager.Turn.InputTurn)
        {
            BattleManager._theTurn = BattleManager.Turn.PlayerTurn;



        }
    }
}
