using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    [SerializeField] CardDataContainer cardDataContainer;
    [SerializeField] TextMeshProUGUI nameField, descriptionField, costField;
    [SerializeField] Image art;

    private void Awake()
    {
        nameField.text = cardDataContainer.cardData.cardName;
        descriptionField.text = cardDataContainer.cardData.description;
        costField.text = cardDataContainer.cardData.cost.ToString();
        art.sprite = cardDataContainer.cardData.art;
    }
}
