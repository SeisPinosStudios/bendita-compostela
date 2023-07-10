using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSelectionManager : MonoBehaviour
{
    public static ShopSelectionManager Instance { get; private set; }

    [SerializeField] GameObject cardPrefab; //Prefab that will be shown when a card is selected, only contains CardDisplay script
    [SerializeField] Transform cardShowcase; //Position where the selected card will be instantiated;
    [SerializeField] GameObject cardInstance; //When a card is selected, this variable references de instantiated card prefab in the showcase slot
    [SerializeField] LayerMask targetLayer;
    [SerializeField] bool isCardSelected;
    [SerializeField] int childSelected;
    public GraphicRaycaster graphicRaycaster;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        SelectCard();
        UnselectCard();
    }

    private void SelectCard()
    {
        if (!Input.GetMouseButtonUp(0)) return;

        if (cardInstance == null) return;

        isCardSelected = true;
    }

    private void UnselectCard()
    {
        if (!Input.GetMouseButtonUp(1)) return;
        OnRightClickClearCard();
    }

    public void ShowCard(CardData cardData, int child)
    {
        if (isCardSelected) return;
        cardPrefab.GetComponent<CardDisplay>().cardData = cardData;
        childSelected = child;
        cardInstance = Instantiate(cardPrefab, cardShowcase);
    }

    public void OnMouseExitClearCard()
    {
        if (isCardSelected) return;
        ClearCardShown();
    }

    public void OnRightClickClearCard()
    {
        if (!isCardSelected) return;
        ClearCardShown();
    }

    private void ClearCardShown()
    {
        Destroy(cardInstance);
        cardInstance = null;
        childSelected = -1;
    }
}
