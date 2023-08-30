using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CondecorationsDisplay : MonoBehaviour
{
    [field: SerializeField] public PlayerData playerData { get; private set; }
    [field: SerializeField] public CondecorationDataContainer condDataContainer { get; private set; }
    [field: SerializeField] public Transform condDisplayObject { get; private set; }

    private void Awake()
    {
        
    }
    private IEnumerator SetupCoroutine()
    {
        yield return new WaitUntil(() => GameManager.Instance.playerData);
        playerData = GameManager.Instance.playerData;
        foreach(CondecorationData condecoration in playerData.condecorations)
        {
            condDataContainer.condecorationData = condecoration;
            Instantiate(condDataContainer, condDisplayObject);
        }
    }
}

