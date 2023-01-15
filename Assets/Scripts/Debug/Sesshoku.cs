using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sesshoku : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogError("さわった");
    }
}
