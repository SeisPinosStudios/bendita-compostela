using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDataContainer : MonoBehaviour
{
    [SerializeField] public EntityData entityData;

    private void Awake()
    {
        if (GetComponent<Entity>() is Player) entityData = GameManager.Instance.playerData;
    }
}
