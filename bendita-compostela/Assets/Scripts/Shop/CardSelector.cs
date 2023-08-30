using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSelector : MonoBehaviour
{
    [SerializeField] CardDataContainer cardDataContainer;
    [SerializeField] CardData cardData;
    

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
        GameManager.Instance.playerData.inventory.Add(cardData.Copy());
    }
}
