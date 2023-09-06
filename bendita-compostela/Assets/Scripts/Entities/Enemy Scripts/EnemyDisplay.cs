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
    }

    private void Update()
    {
        sequenceIcon.sprite = sequenceSprites[(int)enemyBehaviour.sequenceType];
    }
}
