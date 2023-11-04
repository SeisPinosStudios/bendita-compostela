using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseDownsound : MonoBehaviour
{
    [SerializeField] private Sound buttonClickSound;

    // Update is called once per frame
    private void OnMouseDown() 
    {
        SoundManager.Instance.PlaySound(buttonClickSound);
    }
}
