using UnityEngine;
using static AlteredEffectInterface;

public class Burn : IAlteredEffect
{
    public void Effect(EntityEffectsManager entityEffectsManager, Entity entity, GameObject entityGameObject)
    {
        if (!entityEffectsManager.Suffering(TAlteredEffects.AlteredEffects.Burn)) return;

        var burnStacks = entityEffectsManager.alteredEffects[TAlteredEffects.AlteredEffects.Burn];
        entity.SufferDamage(burnStacks < 6 ? burnStacks : burnStacks * 2, true);
        entityEffectsManager.RemoveEffect(TAlteredEffects.AlteredEffects.Burn, burnStacks);
    }
}
