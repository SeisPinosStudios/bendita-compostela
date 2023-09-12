using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
using TMPro;
using UnityEngine.SceneManagement;

public class WinScreenManager : MonoBehaviour
{
    [field: SerializeField] public static WinScreenManager Instance { get; private set; }
    [field: SerializeField] public CardDataContainer cardDataContainer { get; private set; }
    [field: SerializeField] public Transform rewardsZone { get; private set; }
    [field: SerializeField] public Toggle deckToggle { get; private set; }
    [field: SerializeField] public static float condecorationChance { get; private set; } = 5.0f;
    [field: SerializeField] public TextMeshProUGUI otherRewards { get; private set; }
    public AsyncOperation sceneLoad;
    public void Awake()
    {
        Instance = this;

        foreach(CardData cardData in GenerateRewards())
        {
            cardDataContainer.cardData = cardData;
            var reward = Instantiate(cardDataContainer, rewardsZone);
            reward.GetComponent<CardCollection>().OnCardChosen += CardChosen;
        }

        GenerateExtraRewards();
        GameManager.Instance.playerData.SetCurrentHP(BattleManager.Instance.player.currentHP);
        StartCoroutine(LoadMapCoroutine());

        
        BattleManager.Instance.soundList.PlaySound("WinSound");
    }

    public IEnumerator Start()
    {
        yield return new WaitForSeconds(BattleManager.Instance.soundList.vfxSoundsList.Find(audio => audio.soundName == "WinSound").AudioClip.length);
        BattleManager.Instance.soundList.PlayMusic("WinMusic");
    }

    private List<CardData> GenerateRewards()
    {
        var objects = SODataBase.objects;
        var rewardList = new List<CardData>();

        rewardList.Add(objects[UnityEngine.Random.Range(0, objects.Count)]);
        rewardList.Add(objects[UnityEngine.Random.Range(0, objects.Count)]);
        rewardList.Add(objects[UnityEngine.Random.Range(0, objects.Count)]);

        return rewardList;
    }
    private void GenerateExtraRewards()
    {
        var text = new StringBuilder();
        var coins = 0;
        foreach (EnemyData enemy in BattleManager.Instance.combatData.enemiesData)
            coins += enemy.reward;

        GameManager.Instance.playerData.AddCoins(coins);
        text.Append($"{coins} monedas.");

        var obtainedCond = UnityEngine.Random.Range(0, 100) < condecorationChance;
        var condecoration = SODataBase.obtainableCondecorations[UnityEngine.Random.Range(0, SODataBase.obtainableCondecorations.Count)];
        if (obtainedCond)
        {
            GameManager.Instance.playerData.AddCondecoration(condecoration);
        }

        if (obtainedCond) text.Append($"{condecoration.condecorationName}:{condecoration.description}");

        otherRewards.text = text.ToString();
    }
    private void CardChosen()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator LoadMapCoroutine()
    {
        sceneLoad = SceneManager.LoadSceneAsync("Map");
        sceneLoad.allowSceneActivation = false;
        yield return null;
    }
}
