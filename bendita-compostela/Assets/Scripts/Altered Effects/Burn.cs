using UnityEngine;
using static BasicAlteredEffect;

public class Burn : BasicAlteredEffect
{
    public static void Effect(EntityEffectsManager entityEffectsManager, Entity entity, GameObject entityGameObject, Object data)
    {
        if (!entityEffectsManager.Suffering(TAlteredEffects.AlteredEffects.Burn)) return;

        var burnStacks = entityEffectsManager.alteredEffects[TAlteredEffects.AlteredEffects.Burn];
        entity.SufferDamage(burnStacks < entityEffectsManager.burnThreshold ? burnStacks : burnStacks * 2, 0, 0.0f, true);
        entityEffectsManager.RemoveEffect(TAlteredEffects.AlteredEffects.Burn, burnStacks);
    }

    public static string GetDescription(EntityEffectsManager entityEffManager, Entity entity)
    {
        var burnStacks = entityEffManager.alteredEffects[TAlteredEffects.AlteredEffects.Burn];
        var finalDamage = burnStacks > entityEffManager.burnThreshold ? burnStacks * 2 : burnStacks;
        return $"<sprite=5> Quemado: Al final del turno, sufre tanto daño como cargas de quemado tengas. A partir de {entityEffManager.burnThreshold}" +
            $"cargas el daño se duplica. Daño actual: {finalDamage}";
    }
}
