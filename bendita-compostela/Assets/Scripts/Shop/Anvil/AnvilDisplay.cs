using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnvilDisplay : MonoBehaviour
{
    [SerializeField] SpriteRenderer highlight;
    [SerializeField] Animator hammerAnimator;
    [field: SerializeField] public GameObject anvilScreen { get; private set; }
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
        anvilScreen.SetActive(true);
        ShopSelectionManager.Instance.DisableInteraction();
    }
}
