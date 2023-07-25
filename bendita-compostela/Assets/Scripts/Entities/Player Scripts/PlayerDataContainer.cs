using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataContainer : MonoBehaviour
{
    [field:SerializeField] public PlayerData playerData { get; private set; }
}
