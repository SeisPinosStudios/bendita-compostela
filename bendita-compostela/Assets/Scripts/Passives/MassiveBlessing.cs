using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassiveBlessing : BasicPassive
{
    [field:SerializeField] public Enemy enemy { get; private set; }
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        TurnManager.Instance.OnTurn += CheckHealth;
        Heal.OnHeal += PassiveEffect;
    }

    private void CheckHealth()
    {
        if (enemy.IsDamaged(0.5f)) Destroy(this);
    }
    private void PassiveEffect(CardData card, GameObject user)
    {
        if (user.GetComponent<Entity>().GetType() != typeof(Enemy)) return;
        foreach (Enemy enemy in BattleManager.Instance.enemies) 
            if (enemy != this.enemy) 
                enemy.RestoreHealth(int.Parse(card.GetHeal()), enemy.healingBonus, enemy.healingMultiplier);
    }
}
