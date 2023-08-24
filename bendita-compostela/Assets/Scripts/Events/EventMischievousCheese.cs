using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventMischievousCheese : MonoBehaviour
{
    [SerializeField] private EventText eventText;
    [SerializeField] private EventText cheesesList;
    [SerializeField] private Dictionary<string,Sprite> cheesesImageDictionary = new Dictionary<string, Sprite>();
    [SerializeField] private List<Sprite> cheesesImageList = new List<Sprite>();
    [SerializeField] private Image[] pivotImages;

    [SerializeField] private int selectedCheese;

    private void Start() {        
        selectedCheese = Random.Range(0,cheesesList.text.Count);
        for (int i = 0; i < pivotImages.Length; i++)
        {
            pivotImages[i].sprite = cheesesImageList[i];            
        }
    }

    public void CheeseClicked(int i)
    {        
        Debug.Log($"i = {i}");
        Debug.Log($"selected cheese = {selectedCheese}");
        if(selectedCheese == i)
        {
            Debug.Log("WIn");
        }
    }
 }