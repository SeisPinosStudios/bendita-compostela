using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : BaseWeapon
{
    private void Awake()
    {
        weaponId = 4;
        Damage.onAttack += Style;
    }

    private void Style(GameObject target, CardData card)
    {
        var index = BattleManager.Instance.enemies.IndexOf(target.GetComponent<Enemy>());
        if (index == BattleManager.Instance.enemies.Count - 1) return;
        BattleManager.Instance.enemies[index+1].SufferDamage(Mathf.RoundToInt(int.Parse(card.GetDamage())*0.5f), false);
    }
}
