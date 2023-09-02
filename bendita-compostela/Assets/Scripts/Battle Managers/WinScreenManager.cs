using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreenManager : MonoBehaviour
{
    [field: SerializeField] public static WinScreenManager Instance { get; private set; }
    [field: SerializeField] public CardDataContainer cardDataContainer { get; private set; }
    [field: SerializeField] public Transform rewardsZone { get; private set; }
    [field: SerializeField] public Toggle deckToggle { get; private set; }
    [field: SerializeField] public static float condecorationChance { get; private set; }
    public void Awake()
    {
        Instance = this;

        foreach(CardData cardData in GenerateRewards())
        {
            cardDataContainer.cardData = cardData;
            var reward = Instantiate(cardDataContainer, rewardsZone);
            reward.GetComponent<CardCollection>().OnCardChosen += CardChosen;
        }
    }

    private List<CardData> GenerateRewards()
    {
        var objects = SODataBase.objects;
        var rewardList = new List<CardData>();

        rewardList.Add(objects[Random.Range(0, objects.Count)]);
        rewardList.Add(objects[Random.Range(0, objects.Count)]);
        rewardList.Add(objects[Random.Range(0, objects.Count)]);

        return rewardList;
    }
    private void GenerateExtraRewards()
    {

    }
    private void CardChosen()
    {
        gameObject.SetActive(false);
    }
}
