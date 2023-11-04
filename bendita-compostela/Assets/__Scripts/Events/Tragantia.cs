using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Tragantia : BookEventController
{
    public void Option1()
    {

    }

    public void Option2()
    {
        GameManager.Instance.playerData.inventory.RemoveAll(card => card.AppliesEffect(TAlteredEffects.AlteredEffects.Poison));
    }

    public void Option3()
    {
        GameManager.Instance.playerData.ChangeCurrentHP(-7);
    }
}
