using UnityEngine;
using static AlteredEffectInterface;

public class Burn : IAlteredEffect
{
    public void Effect(EntityEffectsManager entityEffectsManager, Entity entity, GameObject entityGameObject, Object data)
    {
        if (!entityEffectsManager.Suffering(TAlteredEffects.AlteredEffects.Burn)) return;

        var burnStacks = entityEffectsManager.alteredEffects[TAlteredEffects.AlteredEffects.Burn];
        entity.SufferDamage(burnStacks < entityEffectsManager.burnThreshold ? burnStacks : burnStacks * 2, 0, 0.0f, true);
        entityEffectsManager.RemoveEffect(TAlteredEffects.AlteredEffects.Burn, burnStacks);
    }
}
