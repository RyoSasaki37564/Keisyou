using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerQTE : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DQTE(int contact, int trueID, int damage)
    {
        if(contact != trueID)
        {
            Player.Instance.Damage(damage, true);
        }
    }
}
