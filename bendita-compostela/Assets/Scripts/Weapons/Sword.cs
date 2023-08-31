using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : BaseWeapon
{
    [field:SerializeField] public int styleAttacks { get; private set; }
    [field:SerializeField] public int chestLevel { get; private set; }
    [field:SerializeField] public float styleMultiplier { get; private set; }

    private void Awake()
    {
        weaponId = 0;
        player = GetComponent<Player>();
        chestSynergy = player.playerData.chestArmor.weaponSynergy == weaponId;
        legSynergy = player.playerData.legArmor.weaponSynergy == weaponId;
        chestLevel = player.playerData.chestArmor.synergyLevel;

        styleMultiplier = player.weapon.styleLevel == 0 ? 0.5f : player.weapon.styleLevel;

        Damage.OnAttack += Style;
        TurnManager.Instance.onTurn += Turn;
        Turn();
    }

    private void Style(GameObject target, CardData card)
    {
        if (TurnManager.Instance.entityTurn.GetType() != typeof(PlayerBehaviour)) return;

        if (styleAttacks > 0) { styleAttacks--; return; }

        player.DamageMultiplier(styleMultiplier);
    }

    private void Turn()
    {
        styleAttacks = 1 + (chestSynergy ? (1 * chestLevel) + 1 : 0);
        player.DamageMultiplier(styleMultiplier);
    }
}
