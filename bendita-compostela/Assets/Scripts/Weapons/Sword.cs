using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : BaseWeapon
{
    [field: SerializeField] public int styleAttacks { get; private set; }
    [field: SerializeField] public int chestLevel { get; private set; }

    private void Awake()
    {
        weaponId = 1;
        player = GetComponent<Player>();
        chestSynergy = player.playerData.chestArmor.weaponSynergy == weaponId;
        feetSynergy = player.playerData.legArmor.weaponSynergy == weaponId;

        Damage.onAttack += Style;
        TurnManager.Instance.onTurn += Turn;
        Turn();
    }

    private void Style(GameObject target, CardData card)
    {
        if (styleAttacks > 0) { styleAttacks--; return; }
        player.DamageBoost(-50.0f);
    }

    private void Turn()
    {
        styleAttacks = 2 + (chestSynergy ? 1 * chestLevel : 0);
        player.DamageBoost(50.0f);
    }
}
