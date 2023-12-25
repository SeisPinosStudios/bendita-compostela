using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Verses : PoemEffect
{
    public static new void Effect()
    {
        DeckManager.Instance.SetCardsToDraw(10);
    }
}
