using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testanim2 : MonoBehaviour
{
    [SerializeField] private Animator anim;
    public void HighlightOff() 
    {
        anim.SetLayerWeight(5,0);
    }
    public void HighlightOn()
    {
        anim.SetLayerWeight(5, 1);
    }
}
