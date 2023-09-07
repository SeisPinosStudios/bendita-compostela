using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boogey : BasicPassive
{
    public void Start()
    {
        TurnManager.Instance.playerBehaviour.OnPlayerTurn += Effect;
    }

    private void Effect()
    {
        if(!BattleManager.Instance.player.weapon) return;
        BattleManager.Instance.player.entityEffectsManager.ApplyEffect(TAlteredEffects.AlteredEffects.Disarmed, 1);
    }

    private void OnDestroy()
    {
        TurnManager.Instance.playerBehaviour.OnPlayerTurn -= Effect;
    }

    #region Description
    public static string GetDescription()
    {
        return $"Intimidaci�n del Saco: cuando Mat�as se equipe un arma pasar� a sufrir Desarmado <sprite=7> durante el resto del combate";
    }
    #endregion
}
