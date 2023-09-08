using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnvilDisplay : MonoBehaviour
{
    [SerializeField] SpriteRenderer highlight;
    [SerializeField] Animator hammerAnimator;
    [field: SerializeField] public GameObject anvilScreen { get; private set; }
    [SerializeField] private Sound anvilsound;
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
        SoundManager.Instance.PlaySound(anvilsound.AudioClip, anvilsound.Volume);
        hammerAnimator.Play("HammerAnimation"); 
        //TODO no hacerlo con invoke
        Invoke("ChangeAnvilWindow", 0.7f);        
    }
    void ChangeAnvilWindow()
    {
        anvilScreen.SetActive(true);
        ShopSelectionManager.Instance.DisableInteraction();
    }
}
