using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using System;
using UnityEngine.SceneManagement;

public class CardDisplay : MonoBehaviour
{
    [SerializeField] CardDataContainer cardDataContainer;
    [field:SerializeField] public CardData cardData { get; private set; }
    [SerializeField] TextMeshProUGUI nameField, descriptionField, costField;
    [SerializeField] Image art;
    public bool dragging;

    Entity target;
    int finalDamage;
    StringBuilder descriptionBuilder;

    private void Awake()
    {
        cardData = cardDataContainer.cardData;
        nameField.text = cardData.cardName;
        descriptionField.text = cardData.description;
        costField.text = cardData.cost == 0 ? "" : cardData.cost.ToString();
        art.sprite = cardData.art;

        if (cardData is WeaponData or ArmorData) costField.transform.parent.gameObject.SetActive(false);
    }

    public void BuildDescription()
    {
        var description = new StringBuilder();

        foreach(CardData.Effect effect in cardData.cardEffects)
        {
            description.Append(GetDescription(effect) != null ? GetDescription(effect) : "");
            description.Append(" ");
        }

        descriptionField.text = description.ToString();
    }
    private string GetDescription(CardData.Effect effect)
    {
        var entity = SceneManager.GetActiveScene().name == "Battle" ? TurnManager.Instance.entityTurn.entity : null;

        var description = (string)Type.GetType(effect.ToString()).GetMethod("GetDescription")
            .Invoke(null, new object[] { cardDataContainer.cardData, entity, target ? target : null });

        return description;
    }
    
    private void Update()
    {
        if (dragging && RaycastUtils.Raycast2D() && RaycastUtils.Raycast2D().GetComponent<Enemy>()) 
            target = RaycastUtils.Raycast2D().GetComponent<Enemy>();
        else target = null;

        BuildDescription();
    }
}
