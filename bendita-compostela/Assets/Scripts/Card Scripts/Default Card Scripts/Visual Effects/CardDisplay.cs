using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class CardDisplay : MonoBehaviour
{
    [SerializeField] CardDataContainer cardDataContainer;
    [field:SerializeField] public CardData cardData { get; private set; }
    [SerializeField] TextMeshProUGUI nameField, descriptionField, costField;
    [SerializeField] Image art;

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

    private void CalculateFinalDamage()
    {
        var user = TurnManager.Instance.entityTurn.GetComponent<Entity>();

        var finalDamage = (cardData.GetDamage() + user.damageBonus - (target ? target.defenseBonus : 0) * (user.GetAttackMultiplier() - (target ? target.GetDefenseMultiplier() : 0)));
    }
    private void GetDescription()
    {

    }
    private void Update()
    {
        if (RaycastUtils.Raycast2D().GetComponent<Enemy>()) target = RaycastUtils.Raycast2D().GetComponent<Enemy>();
        else target = null;
    }
}
