using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTE : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(QTEBited());
    }

    IEnumerator QTEBited()
    {
        yield return new WaitForSeconds(1.5f);
        Player._pHP -= 10;
        Destroy(this.gameObject);
    }

    private void OnMouseEnter()
    {
        Destroy(this.gameObject);
    }
}
