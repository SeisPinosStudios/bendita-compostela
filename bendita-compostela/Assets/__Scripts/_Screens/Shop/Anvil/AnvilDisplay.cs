using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnvilDisplay : MonoBehaviour
{
    [SerializeField] SpriteRenderer highlight;
    [SerializeField] Animator hammerAnimator;
    [field: SerializeField] public GameObject anvilScreenPrefab { get; private set; }
    [SerializeField] private Sound anvilsound;
    GameObject anvilScreen;
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
        if (anvilScreen != null) return;

        SoundManager.Instance.PlaySound(anvilsound.AudioClip, anvilsound.Volume);
        hammerAnimator.Play("HammerAnimation"); 
        //TODO no hacerlo con invoke
        Invoke("ChangeAnvilWindow", 0.7f);        
    }
    void ChangeAnvilWindow()
    {
        anvilScreenPrefab.GetComponent<Canvas>().worldCamera = Camera.main;
        anvilScreenPrefab.GetComponent<Canvas>().sortingLayerName = "UI";
        anvilScreen = Instantiate(anvilScreenPrefab);
        ShopSelectionManager.Instance.DisableInteraction();
    }
}
