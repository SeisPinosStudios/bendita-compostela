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
}
