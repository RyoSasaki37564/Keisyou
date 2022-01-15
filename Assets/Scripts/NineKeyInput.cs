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
                Debug.Log(hit.collider.gameObject.name + " " + GetAngle(hit.collider.gameObject.transform.position, hit.normal));
            }
        }
    }
    float GetAngle(Vector2 start, Vector2 target)
    {
        Vector2 dt = target - start;
        float rad = Mathf.Atan2(dt.y, dt.x);
        float degree = rad * Mathf.Rad2Deg;

        return degree;
    }
}