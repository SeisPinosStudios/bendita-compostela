using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOnclickSound : MonoBehaviour
{
    [SerializeField] private Sound buttonClickSound;

    public void ClickSound()
    {
        SoundManager.Instance.PlaySound(buttonClickSound);
    }
}
