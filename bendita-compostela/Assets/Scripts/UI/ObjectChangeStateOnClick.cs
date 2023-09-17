using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectChangeStateOnClick : MonoBehaviour
{
    [SerializeField] private GameObject objectToChange;
    public bool activeState = false;

    public void ChangeState() 
    {
        activeState = !activeState;
        objectToChange.SetActive(activeState);
    }

}
