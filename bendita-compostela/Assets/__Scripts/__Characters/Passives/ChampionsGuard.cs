using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionsGuard : MonoBehaviour
{
    private void Awake()
    {
        Damage.OnAttack += Effect;
    }
    
    private void Effect(GameObject target, GameObject user, CardData card)
    {
        if (target.GetComponent<Entity>() != GetComponent<Entity>()) return;
        if (!GetComponent<Enemy>().entityEffectsManager.Suffering(TAlteredEffects.AlteredEffects.Guarded)) return;
        BattleManager.Instance.player.SufferDamage(Mathf.RoundToInt(card.GetDamage() * 0.5f), 0, 1, false);
    }

    #region Description
    public static string GetDescription()
    {
        return $"En Guardia de Campeón: atacar a El Cid mientras está En Guardia <sprite=3> devolverá el 50% del daño del ataque";
    }
    #endregion
}
