using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vieja : BookEventController
{
    [field: SerializeField, Header("Vieja Event")] public CardData rewardCard { get; private set; }
    [field: SerializeField] public CondecorationData rewardCond { get; private set; }
    public void Option1()
    {
        GameManager.Instance.playerData.inventory.Add(rewardCard.Copy());
    }

    public void Option2()
    {
        GameManager.Instance.playerData.AddCondecoration(rewardCond);
    }

    public void Option3()
    {
        GameManager.Instance.playerData.ChangeCurrentHP(Mathf.RoundToInt(GameManager.Instance.playerData.HP * 0.35f));
    }

}
