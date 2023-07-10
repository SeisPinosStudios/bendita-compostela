using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public CardData cardData;
    [SerializeField] TextMeshProUGUI nameField, descriptionField, costField;
    [SerializeField] Image art;

    private void Awake()
    {
        nameField.text = cardData.cardName;
        descriptionField.text = cardData.description;
        costField.text = cardData.cost.ToString();
        art.sprite = cardData.art;
    }
}
