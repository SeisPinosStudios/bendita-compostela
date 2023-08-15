using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EntityDisplay : MonoBehaviour
{
    [SerializeField] EntityDataContainer entityDataContainer;
    [SerializeField] EntityData entityData;
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
            alteredEffectDisplay.value = effect.Value;
            Instantiate(alteredEffectDisplay, alteredEffectsZone);
        }
    }
}
