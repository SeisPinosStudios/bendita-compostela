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
    [SerializeField] Animator cardAnimator;
    public bool dragging;

    [field: SerializeField] public Entity target;
    int finalDamage;
    StringBuilder descriptionBuilder;

    private void Awake()
    {
        cardData = cardDataContainer.cardData;
        nameField.text = cardData.cardName;
        descriptionField.text = cardData.description;
        costField.text = cardData.cost.ToString();
        art.sprite = cardData.art;

        BattleManager.Instance.player.OnEnergyValueChanged += AnimationDisplay;
        AnimationDisplay(BattleManager.Instance.player.energy);

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

        if(SceneManager.GetActiveScene().name == "Battle")
            costField.text = TurnManager.Instance.entityTurn.entity.entityEffectsManager.Suffering(TAlteredEffects.AlteredEffects.Exhaust) 
                                ? (cardData.cost + 1).ToString() : cardData.cost.ToString();
        else costField.text = cardData.cost.ToString();
    }
    private string GetDescription(CardData.Effect effect)
    {
        var entity = SceneManager.GetActiveScene().name == "Battle" ? TurnManager.Instance.entityTurn.entity : null;

        var description = (string)Type.GetType(effect.ToString()).GetMethod("GetDescription")
            .Invoke(null, new object[] { cardDataContainer.cardData, entity, target ? target : null });

        return description;
    }
    private void AnimationDisplay(int newValue) 
    {
        if (cardAnimator == null) return;
        if (newValue>= cardData.cost) cardAnimator.SetBool("isUsable", true);
        else cardAnimator.SetBool("isUsable", false);
    }
    
    private void Update()
    {
        BuildDescription();

        if (dragging && RaycastUtils.Raycast2D() && RaycastUtils.Raycast2D().GetComponent<Enemy>()) 
            target = RaycastUtils.Raycast2D().GetComponent<Enemy>();
        else target = null;
    }
}
