using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnvilController : MonoBehaviour
{
    [SerializeField] SpriteRenderer highlight;
    [SerializeField] Animator hammerAnimator;
    private void OnMouseEnter()
    {
        highlight.enabled = true;
    }
    private void OnMouseExit()
    {
        highlight.enabled = false;
    }
    private void OnMouseUp()
    {
        hammerAnimator.Play("HammerAnimation");
    }
}
