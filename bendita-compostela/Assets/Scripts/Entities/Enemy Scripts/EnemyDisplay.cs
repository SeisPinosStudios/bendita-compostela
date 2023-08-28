using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDisplay : EntityDisplay
{
    [field: SerializeField, Header("Enemy Display")] public List<Sprite> sequenceSprites { get; private set; }
    [field: SerializeField] public EnemyBehaviour enemyBehaviour { get; private set; }
    [field: SerializeField] public GameObject displayObject { get; private set; }
    [field: SerializeField] public Image sequenceIcon { get; private set; }
    [field: SerializeField, Header("Boss Sextion")] public Sprite bossBarBackground { get; private set; }
    [field: SerializeField] public Sprite bossBarFill { get; private set; }
    [field: SerializeField] public Image healthbarBackground {get; private set; }
    [field: SerializeField] public Image healthbarFill { get; private set; }
    [field: SerializeField] EnemyData enemyData;

    private void Awake()
    {
        displayObject.AddComponent(typeof(PolygonCollider2D));
        enemyData = (EnemyData)entityData;

        if (enemyData.isBoss)
        {
            healthbarBackground.sprite = bossBarBackground;
            healthbarFill.sprite = bossBarFill;
        }
    }

    private void Update()
    {
        sequenceIcon.sprite = sequenceSprites[(int)enemyBehaviour.sequenceType];
    }
}
