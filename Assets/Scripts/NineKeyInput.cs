using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NineKeyInput : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider)
            {
                Debug.Log(hit.collider.gameObject.transform.parent.name + " " + hit.collider.gameObject.name);
            }
        }
    }
}