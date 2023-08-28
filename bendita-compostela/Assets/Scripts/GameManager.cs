using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [field:SerializeField] public static GameManager Instance { get; private set; }
    [field: SerializeField, Header("Player Data")] private PlayerData playerDataPreset;
    [field:SerializeField] public PlayerData playerData { get; private set; }
    [field:SerializeField] public CombatData combatData { get; private set; }

    private void Awake()
    {
        if(!Instance) { Instance = this; DontDestroyOnLoad(gameObject); }
        else Destroy(this.gameObject);

        playerData = playerDataPreset.Copy();
    }

    public void SetCombat(CombatData combatData)
    {
        this.combatData = combatData;
    }
}
