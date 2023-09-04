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
    [SerializeField] public Animator entityAnimator;

    private void Awake()
    {
        //entityData = entityDataContainer.entityData;
        alteredEffectsZone.GetComponentInParent<Canvas>().worldCamera = Camera.main;
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
    public void HitAnimation()
    {
        Debug.Log("HITTED ANIMATION");
        entityAnimator.SetTrigger("Hitted");
        entityAnimator.SetInteger("SlashType",Random.Range(0,8));
        entityAnimator.SetInteger("SlashType",0);        
    }
    public void AttackAnimation()
    {
        entityAnimator.SetTrigger("Attack");
    }    
    public void Bleed()
    {
        entityAnimator.SetInteger("BleedType",Random.Range(0,8));        
        entityAnimator.SetInteger("BleedType",0);                
    }
    public void Burn()
    {
        entityAnimator.SetTrigger("Burn");
    }
    public void Poison()
    {
        entityAnimator.SetTrigger("Poison");
    }
    public void Heal()
    {
        entityAnimator.SetTrigger("Heal");
    }
    public void Vulnerable()
    {
        entityAnimator.SetTrigger("Vulnerable");
    }
    //OnlyPlayer
    public void SetWeaponDisplay()
    {

    }
}
