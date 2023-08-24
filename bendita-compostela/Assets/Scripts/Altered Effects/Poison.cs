using UnityEngine;
using static AlteredEffectInterface;

public class Poison : IAlteredEffect
{
    public void Effect(EntityEffectsManager entityEffectsManager, Entity entity, GameObject entityGameObject, Object data)
    {
        if (!entityEffectsManager.Suffering(TAlteredEffects.AlteredEffects.Poison)) return;

        var accumPoison = entityEffectsManager.accumPoison;
        entity.SufferDamage(accumPoison, 0, 0.0f, true);
        entityEffectsManager.accumPoison = Mathf.Clamp(entityEffectsManager.accumPoison + 2, 0, 5);
    }
}
