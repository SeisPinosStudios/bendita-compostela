using UnityEngine;
using static AlteredEffectInterface;

public class Bleed : IAlteredEffect
{
    public void Effect(EntityEffectsManager entityEffectsManager, Entity entity, GameObject entityGameObject)
    {
        var bleedStacks = entityEffectsManager.alteredEffects[TAlteredEffects.AlteredEffects.Bleed];
        entity.SufferDamage(bleedStacks, true);
        entityEffectsManager.RemoveEffect(TAlteredEffects.AlteredEffects.Bleed, 1);
    }
}
