using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookEventController : MonoBehaviour
{
    public GameObject eventPanel;

    public Sprite nextPage;
    public Image bookImage;

    public void EndEvent()
    {
        Destroy(eventPanel);
    }
    public void TurnPage()
    {
        bookImage.sprite = nextPage;        
    }   
}
