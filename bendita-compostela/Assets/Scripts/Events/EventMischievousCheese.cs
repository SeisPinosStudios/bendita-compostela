using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;
using System.Threading;

public class EventMischievousCheese : MonoBehaviour
{
    [SerializeField] private EventText eventText;
    [SerializeField] private EventText cheesesList;
    [SerializeField] private Dictionary<string,Sprite> cheesesImageDictionary = new Dictionary<string, Sprite>();
    [SerializeField] private List<Sprite> cheesesImageList = new List<Sprite>();
    [SerializeField] private Transform[] pivots;
    [SerializeField] private GameObject cheesePrefab;
    [SerializeField] private TMP_Text textBox;

    [SerializeField] private string selectedCheese;    

    [SerializeField] private Image selectedCheeseDisplay;
    [SerializeField] private GameObject itsOverButton;
    [SerializeField] private GameObject exitButton;

    public GameObject eventPanel;

    public TimerUtils timer;
    private int textIdx = 0;
    private string cheesePicked;

    private void Start() {        
        //Set timer
        timer.SetTimer(15);
        //Create and instantiate cheeses
        for (int i = 0; i < cheesesImageList.Count; i++)
        {
            cheesesImageDictionary.Add(cheesesList.text[i],cheesesImageList[i]);
        }        
        int k = 0;
        //TODO make the position random
        foreach (string cheeseName in cheesesImageDictionary.Keys)
        {            
            var cheese = Instantiate(cheesePrefab,pivots[k]);    
            cheese.GetComponent<Cheese>().name = cheeseName;
            cheese.GetComponent<Cheese>().cheeseName = cheeseName;
            cheese.GetComponent<Cheese>().eventController = this;
            cheese.GetComponent<Image>().sprite = cheesesImageDictionary[cheeseName];
            k++;
        }
    }
    public void StartEvent()
    {        
        //Select Random Cheese
        selectedCheese = cheesesList.text[Random.Range(0,cheesesList.text.Count)];

        //Display TextBox
        TextBoxClicked(); 

        timer.StartTimer();
                
    }

    private void Update() {
        if (timer.IsTimerFinished())
        {
            ConfirmCheese();            
        }
    }

    public void CheeseClicked(string cheese)
    {        
        selectedCheeseDisplay.sprite = cheesesImageDictionary[cheese];
        cheesePicked = cheese;
    }
    public void ConfirmCheese()
    {
        timer.StopTimer();
        timer.ResetTimer();
        if(cheesePicked == selectedCheese)
        {
            Debug.Log("PREMIO");
            textBox.text = "¡Enhorabuena joven peregrino!, sabía que podías estar a la altura de mis quesos ";
        }
        else
        {
            Debug.Log("L");
            textBox.text = "No mereces mis quesos joven peregrino, ¡largo de aquí! Has echado a perder mi tiempo";
        }
        itsOverButton.SetActive(true);
        exitButton.SetActive(true);

    }
    public void TextBoxClicked()
    {               
        if(textIdx >= eventText.text.Count) return;
        if(textIdx == 2)textBox.text = eventText.text[textIdx] + selectedCheese;   
        else textBox.text = eventText.text[textIdx];        
        textIdx++;
    }

    public void EndEvent()
    {
        Destroy(eventPanel);
    }
 }