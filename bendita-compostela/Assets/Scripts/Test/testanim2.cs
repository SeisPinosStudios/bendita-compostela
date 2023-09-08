using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testanim2 : MonoBehaviour
{    
    private void Start() 
    {
        GetComponent<IntentionPosChange>().SetUpPosition();
    }
}
