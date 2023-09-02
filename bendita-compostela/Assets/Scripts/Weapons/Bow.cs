using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : BaseWeapon
{
    [field: SerializeField] public int styleAttacks { get; private set; }
    [field: SerializeField] public int styleAttacksThreshold { get; private set; }
    [field: SerializeField] public int styleRecover { get; private set; }
    [field: SerializeField] public int synergyDraws { get; private set; }
    private void Awake()
    {
        Damage.OnAttack += Style;
        AttackDeckManager.Instance.OnCardDraw += ChestSynergy;

        player = BattleManager.Instance.player;

        styleAttacksThreshold = GetStyleLevel() < 2 ? 5 : 4;
        styleRecover = GetStyleLevel() > 0 ? 4 : 3;

        chestSynergy = GetChestSynergy();
        legSynergy = GetLegSynergy();

        if (chestSynergy && GetChestLevel() == 2) AttackDeckManager.Instance.AddFreeDraw(1);
        if (legSynergy) LegSynergy();
    }

    private void Style(GameObject target, CardData card)
    {
        if (target.GetComponent<Entity>().GetType() == typeof(Player)) return;

        if(styleAttacks < styleAttacksThreshold)
        {
            styleAttacks++;
            return;
        }

        player.RestoreEnergy(styleRecover);
    }

    private void ChestSynergy()
    {
        if (!chestSynergy) return;
        if (synergyDraws < 1 - GetChestLevel()) synergyDraws++;
        AttackDeckManager.Instance.AddFreeDraw(1);
    }

    private void LegSynergy()
    {
        player.AddMaxEnergy(2 * (GetLegLevel() + 1));
    }
}
