using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSelectionManager : MonoBehaviour
{
    public static ShopSelectionManager Instance { get; private set; }

    [SerializeField] CardDataContainer cardPrefab; //Prefab that will be shown when a card is selected, only contains CardDisplay script
    [SerializeField] Transform cardShowcase; //Position where the selected card will be instantiated;
    [SerializeField] GameObject cardInstance; //When a card is selected, this variable references de instantiated card prefab in the showcase slot

    private void Awake()
    {
        Instance = this;
    }

    public void ShowCard(CardData cardData)
    {
        cardPrefab.cardData = cardData;
        var instance = Instantiate(cardPrefab, cardShowcase);
        cardInstance = instance.gameObject;
    }

    public void ClearCardShown()
    {
        Destroy(cardInstance);
        cardInstance = null;
    }
}
