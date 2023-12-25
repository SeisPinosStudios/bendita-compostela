using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheese : MonoBehaviour
{
    public string cheeseName;
    public EventMischievousCheese eventController;
    public void CheeseClicked()
    {        
        Debug.Log(cheeseName);
        eventController.CheeseClicked(cheeseName);
    }
}
