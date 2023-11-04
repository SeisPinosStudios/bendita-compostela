using UnityEngine;
using static BasicAlteredEffect;

public class Bleed : BasicAlteredEffect
{
    public static void Effect(EntityEffectsManager entityEffectsManager, Entity entity, GameObject entityGameObject, Object data)
    {
        var bleedStacks = entityEffectsManager.alteredEffects[TAlteredEffects.AlteredEffects.Bleed];
        entity.SufferDamage(bleedStacks, 0, 0.0f, true);
        entityEffectsManager.RemoveEffect(TAlteredEffects.AlteredEffects.Bleed, 1);
    }

    public static string GetDescription(EntityEffectsManager entityEffManager, Entity entity)
    {
        return $"<sprite=0> Sangrado: al jugar una carta, sufre tantos puntos de daño como cargas de sangrado tenga. " +
        $"Daño actual {entityEffManager.alteredEffects[TAlteredEffects.AlteredEffects.Bleed]}";
    }

    public static string GetBasicDescription()
    {
        return $"<sprite=0> Sangrado; provoca tanto daño como cargas de sangrado al utilizar una carta";
    }
}
