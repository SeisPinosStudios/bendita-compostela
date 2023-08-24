using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlteredEffectInterface : MonoBehaviour
{
    public interface IAlteredEffect 
    {
        public void Effect(EntityEffectsManager entityEffectsManager, Entity entity, GameObject entityGameObject, Object data);
    }
}
