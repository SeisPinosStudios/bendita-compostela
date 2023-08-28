using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : BaseWeapon
{
    private void Awake()
    {
        weaponId = 4;
        Damage.OnAttack += Style;
    }

    private void Style(GameObject target, CardData card)
    {
        if (target.GetComponent<Entity>().GetType() == typeof(Player)) return;
        var index = BattleManager.Instance.enemies.IndexOf(target.GetComponent<Enemy>());
        if (index == BattleManager.Instance.enemies.Count - 1) return;
        var player = BattleManager.Instance.player;
        BattleManager.Instance.enemies[index+1].SufferDamage(Mathf.RoundToInt(int.Parse(card.GetDamage())*0.5f), player.damageBonus, player.damageMultiplier, false);
    }
}
