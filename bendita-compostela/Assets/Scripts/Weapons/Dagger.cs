using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : BaseWeapon
{
    [field: SerializeField] public int styleBonus;
    [field: SerializeField] public int chestLevel;
    private void Awake()
    {
        weaponId = 3;
        Damage.OnAttack += Style;

        player = BattleManager.Instance.player;

        chestSynergy = GetChestSynergy();
        legSynergy = GetLegSynergy();
    }

    private void Style(GameObject target, CardData card)
    {
        if (BattleManager.Instance.enemies.Count > 1) return;
        styleBonus += 1 + (1 * GetStyleLevel());
        BattleManager.Instance.player.AttackBonus(1);
    }

    private void OnDestroy()
    {
        BattleManager.Instance.player.AttackBonus(-styleBonus);
    }
}
