using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : BaseWeapon
{
    [field: SerializeField] public int styleBonus;
    private void Awake()
    {
        weaponId = 3;
        Damage.onAttack += Style;
    }

    private void Style(GameObject target, CardData card)
    {
        if (BattleManager.Instance.enemies.Count > 1) return;
        styleBonus++;
        BattleManager.Instance.player.AttackBonus(1);
    }

    private void OnDestroy()
    {
        BattleManager.Instance.player.AttackBonus(-styleBonus);
    }
}
