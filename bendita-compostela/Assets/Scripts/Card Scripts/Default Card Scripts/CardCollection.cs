using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardCollection : MonoBehaviour
{
    [field: SerializeField] public CardDataContainer cardDataContainer;
    public event Action OnCardChosen = delegate { };
    
    public void AddCard()
    {
        if (WinScreenManager.Instance.deckToggle.isOn) AddToDeck();
        else AddToInventory();
        OnCardChosen();
    }
    public void AddToInventory()
    {
        GameManager.Instance.playerData.inventory.Add(cardDataContainer.cardData.Copy());
    }
    public void AddToDeck()
    {
        GameManager.Instance.playerData.deck.Add(cardDataContainer.cardData.Copy());
    }
    public void LeaveCombat()
    {
        BattleManager.Instance.LoadScene("Map");
    }
}
