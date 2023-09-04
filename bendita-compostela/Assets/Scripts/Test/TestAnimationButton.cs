using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimationButton : MonoBehaviour
{
    public Animator anim;
    public void Hit()
    {
        anim.SetTrigger("Hitted");
    }
    public void Slash()
    {                               
        anim.SetInteger("SlashType",Random.Range(1,9));                
    }
    public void HitAndSlash()
    {
        Hit();
        Slash();
    }
}
