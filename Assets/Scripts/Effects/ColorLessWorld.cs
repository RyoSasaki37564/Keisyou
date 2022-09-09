using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLessWorld : MonoBehaviour
{
    //アニメーションイベントで呼ぶ自己非アクティブ化スクリプト

    public void WhiteWorld()
    {
        gameObject.SetActive(false);
    }
}
