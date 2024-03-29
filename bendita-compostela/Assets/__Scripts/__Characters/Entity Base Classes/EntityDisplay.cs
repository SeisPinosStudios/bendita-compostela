using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;


public class EntityDisplay : MonoBehaviour
{
    [field: SerializeField] public EntityDataContainer entityDataContainer { get; protected set; }
    [field:SerializeField] public EntityData entityData { get; protected set; }
    [SerializeField] Image healthBar;
    [field: SerializeField] public TextMeshProUGUI healthText { get; protected set; }
    [field: SerializeField] public Transform alteredEffectsZone { get; protected set; }
    [field: SerializeField] public AlteredEffectDisplay alteredEffectDisplay { get; protected set; }
    [SerializeField] public Animator entityAnimator;
    public bool showLogs = true;

    private void Awake()
    {
        entityData = entityDataContainer.entityData;
        alteredEffectsZone.GetComponentInParent<Canvas>().worldCamera = Camera.main;
    }
    public IEnumerator Start() {
        yield return new WaitUntil(() => entityDataContainer.entityData);
    }
    private void Update()
    {
        
    }
    public void UpdateHealth(int max, int current)
    {
        healthBar.fillAmount = (float)current / max;
        healthText.text = $"{current}/{max}";
    }
    public virtual void UpdateAlteredEffectsDisplay(EntityEffectsManager manager)
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

    #region Animation
    public void DisplayEffectAnimation(TAlteredEffects.AlteredEffects effect)
    {
        switch (effect)
        {            
            case TAlteredEffects.AlteredEffects.Bleed: 
                Bleed();
                break;
            case TAlteredEffects.AlteredEffects.Poison:
                Poison();
                break;
            case TAlteredEffects.AlteredEffects.Vulnerable:
                Vulnerable();
                break;
            case TAlteredEffects.AlteredEffects.Guarded:                
                break;
            case TAlteredEffects.AlteredEffects.Invulnerable:
                break;
            case TAlteredEffects.AlteredEffects.Burn:
                Burn();
                break;
            case TAlteredEffects.AlteredEffects.Exhaust:
            //meter exhaust
                break;
            case TAlteredEffects.AlteredEffects.Disarmed:
                break;
            case TAlteredEffects.AlteredEffects.Marked:
                break;
            case TAlteredEffects.AlteredEffects.Stun:
            //meter anim
                break;
            case TAlteredEffects.AlteredEffects.Lead:
                break;
            case TAlteredEffects.AlteredEffects.Frenzy:
            //meter anim
                break;            
        }
    }

    public void HitAnimation()
    {
        entityAnimator.SetTrigger("Hitted");
        entityAnimator.SetInteger("SlashType",Random.Range(1,9));        
    }
    public void AttackAnimation()
    {
        entityAnimator.SetTrigger("Attack");
    }    
    public void Bleed()
    {        
        Log("Bleed Animation");
        entityAnimator.SetInteger("BleedType",Random.Range(1,9));                   
    }
    public void Burn()
    {
        Log("Burn Animation");
        entityAnimator.SetTrigger("Burn");
    }
    public void Poison()
    {
        Log("Poison Animation");
        entityAnimator.SetTrigger("Poison");
    }
    public void Heal()
    {
        Log("Heal Animation");
        entityAnimator.SetTrigger("Heal");
    }
    public void Vulnerable()
    {
        Log("Vulnerable Animation");
        entityAnimator.SetTrigger("Vulnerable");
    }
    public void Frenzy()
    {
        Log("Frenzy Animation");
        entityAnimator.SetTrigger("Frenzy");
    }
    public void Stun()
    {
        Log("Stun Animation");
        entityAnimator.SetTrigger("Stun");
    }
    public void HighlightOff()
    {
        entityAnimator.SetLayerWeight(5, 0);
    }
    public void HighlightOn()
    {
        entityAnimator.SetLayerWeight(5, 1);
    }
    //OnlyPlayer
    public void SetWeaponDisplay(int weaponId)
    {
        Log($"Weapon {weaponId} Animation");
        entityAnimator.SetInteger("WeaponType", weaponId+1);
    }

    public IEnumerator SetTriggerAnimation(string trigger, int layer) {
        DebugManager.StaticDebug("Entity", $"{trigger} animation on {entityData.entityName}");
        entityAnimator.SetTrigger(trigger);
        yield return new WaitForSeconds(AnimatorUtils.ClipLength(entityAnimator, layer));
    }

    public IEnumerator SetIntegerAnimation(string integer, int value, int layer) {
        DebugManager.StaticDebug("Entity", $"{integer} set as {value} on {entityData.entityName}");
        entityAnimator.SetInteger(integer, value);
        yield return new WaitForSeconds(AnimatorUtils.ClipLength(entityAnimator, layer));
    }
    #endregion

    void Log(object message)
    {
        if(showLogs) Debug.Log(message);
    }
}
