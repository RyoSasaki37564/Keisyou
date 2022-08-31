using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIColTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TouchManager.Began += (info) =>
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null)
            {
                Debug.Log($"{hit.collider.gameObject.name} hit!");
            }
        };
        TouchManager.Moved += (info) =>
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null)
            {
                Debug.Log($"{hit.collider.gameObject.name} hit!");
            }
        };
        TouchManager.Ended += (info) =>
        {

        };
    }
}
