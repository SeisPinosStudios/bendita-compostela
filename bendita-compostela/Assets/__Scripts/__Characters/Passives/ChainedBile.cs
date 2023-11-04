using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainedBile : BasicPassive
{
    [field: SerializeField] public bool passiveActive;
    private void Awake()
    {
        TurnManager.Instance.OnTurn += PassiveEffect;
    }

    private void PassiveEffect()
    {
        passiveActive = false;
        BattleManager.Instance.player.DefenseBonus(-1);

        if (!BattleManager.Instance.player.GetComponent<EntityEffectsManager>().Suffering(TAlteredEffects.AlteredEffects.Poison)) return;

        passiveActive = true;
        BattleManager.Instance.player.DefenseBonus(-1);
    }

    private void OnDestroy()
    {
        if (passiveActive) BattleManager.Instance.player.DefenseBonus(1);
        TurnManager.Instance.OnTurn -= PassiveEffect;
    }

    #region Description
    public static string GetDescription()
    {
        return $"Bílis en Cadena: si Matías sufre Veneno <sprite=1>, todos los ataques le hacen 1 punto mas de daño.";
    }
    #endregion
}
