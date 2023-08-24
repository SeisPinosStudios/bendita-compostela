using UnityEngine;
using static AlteredEffectInterface;

public class Bleed : IAlteredEffect
{
    public void Effect(EntityEffectsManager entityEffectsManager, Entity entity, GameObject entityGameObject, Object data)
    {
        var bleedStacks = entityEffectsManager.alteredEffects[TAlteredEffects.AlteredEffects.Bleed];
        entity.SufferDamage(bleedStacks, 0, 0.0f, true);
        entityEffectsManager.RemoveEffect(TAlteredEffects.AlteredEffects.Bleed, 1);
    }
}
