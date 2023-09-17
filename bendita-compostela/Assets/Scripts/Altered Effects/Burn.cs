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
        return $"<sprite=5> Quemado: Al final del turno, sufre tanto da�o como cargas de quemado tengas. A partir de {entityEffManager.burnThreshold}" +
            $" cargas el da�o se duplica. Da�o actual: {finalDamage}";
    }

    public static string GetBasicDescription()
    {
        return $"<sprite=5> Quemado: provoca  1 punto de da�o por carga al final del turno. A partir de 6 cargas hace el doble de da�o";
    }
}
