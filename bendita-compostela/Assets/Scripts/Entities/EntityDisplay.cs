using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EntityDisplay : MonoBehaviour
{
    [field: SerializeField] public EntityDataContainer entityDataContainer { get; protected set; }
    [field:SerializeField] public EntityData entityData { get; private set; }
    [SerializeField] Image healthBar;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] Transform alteredEffectsZone;
    [SerializeField] AlteredEffectDisplay alteredEffectDisplay;

    private void Awake()
    {
        entityData = entityDataContainer.entityData;
    }
    private void Update()
    {
        
    }
    public void UpdateHealth(int max, int current)
    {
        healthBar.fillAmount = current / max;
        healthText.text = $"{current}/{max}";
    }
    public void UpdateAlteredEffectsDisplay(EntityEffectsManager manager)
    {
        foreach(Transform child in alteredEffectsZone) Destroy(child.gameObject);

        foreach (KeyValuePair<TAlteredEffects.AlteredEffects, int> effect in manager.alteredEffects)
        {
            if (!manager.Suffering(effect.Key)) continue;
            alteredEffectDisplay.effect = effect.Key;
            alteredEffectDisplay.value = $"x{effect.Value}";
            Instantiate(alteredEffectDisplay, alteredEffectsZone);
        }
    }
}
