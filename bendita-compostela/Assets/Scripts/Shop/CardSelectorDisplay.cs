using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectorDisplay : MonoBehaviour
{
    [SerializeField] CardDataContainer cardDataContainer;
    [SerializeField] CardData cardData;
    [field: SerializeField] public CardSelector cardSelector { get; private set; }
    [SerializeField] SpriteRenderer sprite, highlight;
    [SerializeField] GameObject display;
    [SerializeField] Animator animator;
    

    private void Awake()
    {
        
    }

    private void Start()
    {
        cardData = cardDataContainer.cardData;
        sprite.sprite = cardData.miniArt;
    }

    private void OnMouseEnter()
    {
        ShopSelectionManager.Instance.ShowCard(cardData);
        sprite.gameObject.transform.position += new Vector3(0f, 0.1f, 0f);
        highlight.enabled = true;
    }

    private void OnMouseExit()
    {
        ShopSelectionManager.Instance.ClearCardShown();
        sprite.gameObject.transform.position -= new Vector3(0f, 0.1f, 0f);
        highlight.enabled = false;
    }

    private void OnMouseUp()
    {
        if (!cardSelector.interact) return;
        ShopSelectionManager.Instance.ClearCardShown();
        display.SetActive(false);
        animator.Play("BuyCardAnimation");        
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length - 0.5f);
    }
}
