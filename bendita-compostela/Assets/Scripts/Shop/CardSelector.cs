using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelector : MonoBehaviour
{
    [SerializeField] CardDataContainer cardDataContainer;
    [SerializeField] CardData cardData;
    [field: SerializeField] public CardSelectorDisplay cardSelectorDisplay { get; private set; }
    [field: SerializeField] public bool interact { get; private set; }
    [field: SerializeField] public PolygonCollider2D cardCollider { get; private set; }

    [SerializeField] AudioClip buySound;

    private void Awake()
    {
        
    }

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => cardDataContainer.cardData);
        cardData = cardDataContainer.cardData;
    }

    private void OnMouseEnter()
    {
        
    }

    private void OnMouseExit()
    {
        
    }
    private void OnMouseUp()
    {
        if (!interact) return;

        if (!GameManager.Instance.playerData.SpendCoins(cardData.price)) return;

        if (cardData is WeaponData)
        {
            SoundManager.Instance.PlaySound(buySound);
            GameManager.Instance.playerData.inventory.Add(((WeaponData)cardData).Copy());
            cardSelectorDisplay.BuyAnimation();
            return;
        }

        if(cardData is ArmorData)
        {
            SoundManager.Instance.PlaySound(buySound);
            GameManager.Instance.playerData.inventory.Add(((ArmorData)cardData).Copy());
            cardSelectorDisplay.BuyAnimation();
            return;
        }
        SoundManager.Instance.PlaySound(buySound);
        GameManager.Instance.playerData.inventory.Add(cardData.Copy());
        cardSelectorDisplay.BuyAnimation();
    }

    public void Disable()
    {
        cardCollider.enabled = false;
        interact = false;
    }

    public void Enable()
    {
        cardCollider.enabled = true;
        interact = true;
    }
}
