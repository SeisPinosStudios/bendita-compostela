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
    [SerializeField] float offset;

    private void OnMouseDown()
    {
        offset = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - box.position.y;
    }

    private void OnMouseDrag()
    {
        yPosMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        box.localPosition = new Vector3(transform.localPosition.x, yPosMouse-offset, transform.localPosition.z);
    }   

}
