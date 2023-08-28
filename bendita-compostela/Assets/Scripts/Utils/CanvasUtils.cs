using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasUtils : MonoBehaviour
{
    public static Canvas GetMainCanvas()
    {
        return GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
    }
}
