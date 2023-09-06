using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelector : MonoBehaviour
{
    [SerializeField] CardDataContainer cardDataContainer;
    [SerializeField] CardData cardData;
    [field: SerializeField] public bool interact { get; private set; }
    [field: SerializeField] public PolygonCollider2D cardCollider { get; private set; }

    [SerializeField] AudioClip buySound;

    private void Awake()
    {
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

        if(cardData is WeaponData)
        {
            GameManager.Instance.playerData.inventory.Add(((WeaponData)cardData).Copy());
            return;
        }

        if(cardData is ArmorData)
        {
            GameManager.Instance.playerData.inventory.Add(((ArmorData)cardData).Copy());
            return;
        }
        SoundManager.Instance.PlaySound(buySound);
        GameManager.Instance.playerData.inventory.Add(cardData.Copy());
    }

    public void Disable()
    {
        print($"{this.name} Disable");
        cardCollider.enabled = false;
        interact = false;
    }

    public void Enable()
    {
        cardCollider.enabled = true;
        interact = true;
    }
}
