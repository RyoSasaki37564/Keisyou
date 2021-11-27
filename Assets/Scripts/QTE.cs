using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTE : MonoBehaviour
{
    
    public void QTEBited()
    {
        Player.Instance.Damage(10, true);
        Destroy(this.gameObject);
    }

    private void OnMouseEnter()
    {
        Destroy(this.gameObject);
    }
}
