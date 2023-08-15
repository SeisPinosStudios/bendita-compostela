using UnityEngine;
using static AlteredEffectInterface;

public class Poison : IAlteredEffect
{
    public void Effect(EntityEffectsManager entityEffectsManager, Entity entity, GameObject entityGameObject)
    {
        if (!entityEffectsManager.Suffering(TAlteredEffects.AlteredEffects.Poison)) return;

        var accumPoison = entityEffectsManager.accumPoison;
        entity.SufferDamage(accumPoison, true);
        entityEffectsManager.accumPoison = Mathf.Clamp(entityEffectsManager.accumPoison + 2, 0, 5);
    }
}
