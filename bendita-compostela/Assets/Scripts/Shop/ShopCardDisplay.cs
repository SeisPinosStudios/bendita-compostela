using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopCardDisplay : CardDisplay
{
    [SerializeField] TextMeshProUGUI priceTag;

    private void Start()
    {
        priceTag.text = cardData.price.ToString();
    }
}
