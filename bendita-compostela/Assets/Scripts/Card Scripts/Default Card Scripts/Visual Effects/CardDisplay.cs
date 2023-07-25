using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    [SerializeField] CardDataContainer cardDataContainer;
    [field:SerializeField] public CardData cardData { get; private set; }
    [SerializeField] TextMeshProUGUI nameField, descriptionField, costField;
    [SerializeField] Image art;

    private void Awake()
    {
        cardData = cardDataContainer.cardData;
        nameField.text = cardData.cardName;
        descriptionField.text = cardData.description;
        costField.text = cardData.cost == 0 ? "" : cardData.cost.ToString();
        art.sprite = cardData.art;
    }
}
