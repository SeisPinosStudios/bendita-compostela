using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EqPoemsDisplayManager : MonoBehaviour
{
    [field: SerializeField] public static EqPoemsDisplayManager Instance { get; private set; }
    [field: SerializeField] public PlayerData playerData { get; private set; }
    [field: SerializeField] public PoemDataContainer poemDataCont { get; private set; }
    [field: SerializeField] public Transform poemsDisplay { get; private set; }
    [field: SerializeField] public List<Image> equipedPoems { get; private set; }

    private void Awake()
    {
        Instance = this;
        StartCoroutine(SetupCoroutine());
    }

    private IEnumerator SetupCoroutine()
    {
        yield return new WaitUntil(() => GameManager.Instance && GameManager.Instance.playerData);
        playerData = GameManager.Instance.playerData;
        ShowPoems();
    }

    private void ShowPoems()
    {
        foreach(PoemData poem in playerData.poemInventory)
        {
            poemDataCont.poemData = poem;
            Instantiate(poemDataCont, poemsDisplay);
        }
        UpdateEquipedPoems();
    }

    public void UpdateEquipedPoems()
    {
        for(int i = 0; i < playerData.poems.Count; i++)
        {
            equipedPoems[i].sprite = playerData.poems[i].sprite;
        }
    }
}
