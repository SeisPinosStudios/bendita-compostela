using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class MapScroll : MonoBehaviour
{
    float yPosMouse;
    public Transform box;

    private void OnMouseDrag()
    {
        yPosMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        box.localPosition = new Vector3(transform.localPosition.x,
                                              yPosMouse,
                                              transform.localPosition.z);


    }   

}
