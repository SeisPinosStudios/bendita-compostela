using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDisplay : EntityDisplay
{
    [field: SerializeField, Header("Enemy Display")] public SpriteRenderer enemySprite { get; private set; }
    [field: SerializeField] public EnemyData enemyData { get; private set; }
    [field: SerializeField, Header("Enemy Behaviour Display")] public List<Sprite> sequenceSprites { get; private set; }
    [field: SerializeField] public EnemyBehaviour enemyBehaviour { get; private set; }
    [field: SerializeField] public GameObject displayObject { get; private set; }
    [field: SerializeField] public Image sequenceIcon { get; private set; }
    [field: SerializeField, Header("Passives")] public PassiveDisplay passiveDisplay { get; private set; }
    [field: SerializeField] public int passiveindex { get; private set; }
    [field: SerializeField, Header("Boss Section")] public Sprite bossBarBackground { get; private set; }
    [field: SerializeField] public Sprite bossBarFill { get; private set; }
    [field: SerializeField] public Image healthbarBackground {get; private set; }
    [field: SerializeField] public Image healthbarFill { get; private set; }

    private void Awake()
    {
        
        enemyData = (EnemyData)entityDataContainer.entityData;

        enemySprite.sprite = enemyData.enemySprite;
        displayObject.AddComponent(typeof(PolygonCollider2D));
        if (enemyData.isBoss)
        {
            healthbarBackground.sprite = bossBarBackground;
            healthbarFill.sprite = bossBarFill;
        }
        sequenceIcon.gameObject.GetComponent<IntentionPosChange>().SetUpPosition();

        passiveindex = enemyData.passives.Count;
        GeneratePassiveIcons();
    }
    private void Update()
    {
        sequenceIcon.sprite = sequenceSprites[(int)enemyBehaviour.sequenceType];
    }
    public override void UpdateAlteredEffectsDisplay(EntityEffectsManager manager)
    {
        for (int i = passiveindex; i < alteredEffectsZone.childCount; i++) Destroy(alteredEffectsZone.GetChild(i).gameObject);

        foreach (KeyValuePair<TAlteredEffects.AlteredEffects, int> effect in manager.alteredEffects)
        {
            if (!manager.Suffering(effect.Key)) continue;
            alteredEffectDisplay.effect = effect.Key;
            alteredEffectDisplay.value = $"x{effect.Value}";
            Instantiate(alteredEffectDisplay, alteredEffectsZone);
        }
    }
    private void GeneratePassiveIcons()
    {
        foreach (BasicPassive.Passive passive in enemyData.passives)
        {
            passiveDisplay.passive = passive;
            Instantiate(passiveDisplay, alteredEffectsZone);
        }
    }

}
