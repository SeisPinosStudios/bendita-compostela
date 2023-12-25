using UnityEngine;
using static BasicAlteredEffect;

public class Poison : BasicAlteredEffect
{
    public static void Effect(EntityEffectsManager entityEffectsManager, Entity entity, GameObject entityGameObject, Object data)
    {
        if (!entityEffectsManager.Suffering(TAlteredEffects.AlteredEffects.Poison)) return;

        var accumPoison = entityEffectsManager.accumPoison;
        entity.SufferDamage(accumPoison, 0, 0.0f, true);
        entityEffectsManager.accumPoison = Mathf.Clamp(entityEffectsManager.accumPoison + 2, 0, 5);
        entityEffectsManager.RemoveEffect(TAlteredEffects.AlteredEffects.Poison, 1);
    }

    public static string GetDescription(EntityEffectsManager entityEffManager, Entity entity)
    {
        return $"<sprite=1> Veneno: Al principio del turno, sufre daño que va aumentando con cada turno con veneno. " +
            $"Daño actual: {entityEffManager.accumPoison}.";
    }

    public static string GetBasicDescription()
    {
        return $"<sprite=1> Veneno: al principio del turno recibe daño que incrementa con cada turno sufriendo veneno";
    }
}
