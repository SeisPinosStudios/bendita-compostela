using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAlteredEffect : MonoBehaviour
{
    public interface IAlteredEffect
    {
        public string GetDescription(EntityEffectsManager entityEffManager, Entity entity);

        public string GetBasicDescription();
    }
}
