using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoemsDisplay : MonoBehaviour
{
    [field:SerializeField] public PlayerData playerData { get; private set; }
    [field:SerializeField] public PoemDataContainer poemDataContainer { get; private set; }
    [field:SerializeField] public Transform poemDisplayObject { get; private set; }

    private void Awake()
    {
        StartCoroutine(SetupCoroutine());
    }

    private IEnumerator SetupCoroutine()
    {
        yield return new WaitUntil(() => BattleManager.Instance != null && BattleManager.Instance.player != null && BattleManager.Instance.player.playerData != null);
        playerData = BattleManager.Instance.player.playerData;
        foreach(PoemData poemData in playerData.poems)
        {
            poemDataContainer.poemData = poemData;
            Instantiate(poemDataContainer, poemDisplayObject);
        }
        yield return null;
    }
}
