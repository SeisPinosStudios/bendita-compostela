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
    #region Variables
    [SerializeField] private EventText eventText;
    [SerializeField] private EventText cheesesList;
    [SerializeField] private Dictionary<string,Sprite> cheesesImageDictionary = new Dictionary<string, Sprite>();
    [SerializeField] private List<Sprite> cheesesImageList = new List<Sprite>();
    [SerializeField] private List<Sprite> shepardReactionImageList = new List<Sprite>();
    [SerializeField] private Transform[] pivots;
    [SerializeField] private GameObject cheesePrefab;
    [SerializeField] private TMP_Text textBox;
    [SerializeField] private string selectedCheese;    
    [SerializeField] private Image selectedCheeseDisplay;
    [SerializeField] private Image shepardImage;
    private List<GameObject> cheesesGO = new List<GameObject>();

    public Sound cheeseEventMusic;

    public GameObject eventPanel;

    public GameObject nextButton;
    public GameObject rewardWindow;
    
    private int textIdx = 0;
    private string cheesePicked;

    #endregion
   
    #region Event Reward DIEGO AQUI
    public void ConfirmCheese()
    {
        textIdx = eventText.text.Count;
        if(cheesePicked == selectedCheese) 
        {   
            //SI HA SELECCIONADO EL QUESO CORRECTO
            textBox.text = "¡Enhorabuena joven peregrino!, sabía que podías estar a la altura de mis quesos ";
            shepardImage.sprite = shepardReactionImageList[0];
            nextButton.SetActive(true);
            nextButton.GetComponent<Button>().onClick.AddListener(()=>rewardWindow.SetActive(true));
            nextButton.GetComponent<Button>().onClick.AddListener(()=>rewardWindow.GetComponent<EventRewardDisplay>().SetTextReward("Obtuviste carta Queso \nRecuperas un 20% de la vida"));
            GameManager.Instance.playerData.ChangeCurrentHP(Mathf.RoundToInt(GameManager.Instance.playerData.HP * 0.2f));
        }                
        else
        {
            //SI NO LO HA SELECCIONADO
          textBox.text = "No mereces mis quesos joven peregrino, ¡largo de aquí! Has echado a perder mi tiempo";          
          shepardImage.sprite = shepardReactionImageList[1];          
        } 

    }
    #endregion

    #region Event Logic
    private void Start() {                        
        SoundManager.Instance.PlayMusic(cheeseEventMusic.AudioClip, cheeseEventMusic.Volume);

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
            cheesesGO.Add(cheese);
            cheese.SetActive(false);
            k++;
        }
        
        StartEvent();
    }
    public void StartEvent()
    {        
        //Select Random Cheese
        selectedCheese = cheesesList.text[Random.Range(0,cheesesList.text.Count)];

        //Display TextBox
        TextBoxClicked();                 
    }


    public void CheeseClicked(string cheese)
    {        
        selectedCheeseDisplay.sprite = cheesesImageDictionary[cheese];
        selectedCheeseDisplay.color = new Color(1,1,1,1);
        cheesePicked = cheese;
    }
    
    public void TextBoxClicked()
    {                    
        ChangeShepardSprite();
        if(textIdx >= eventText.text.Count) return;
        if(textIdx == 4)
        {
            foreach (GameObject cheese in cheesesGO)
            {
                cheese.SetActive(true);
            }
            textBox.text = eventText.text[textIdx] + selectedCheese;   
        }
        else if(textIdx == 5) 
        {
            nextButton.SetActive(false);
            textBox.text = eventText.text[textIdx];
        }
        else textBox.text = eventText.text[textIdx];        
        textIdx++;
    }

    public void ChangeShepardSprite()
    {
        switch(textIdx)
        {
            case 0: shepardImage.sprite = shepardReactionImageList[2]; break;
            case 1: shepardImage.sprite = shepardReactionImageList[4]; break;
            case 5: shepardImage.sprite = shepardReactionImageList[3]; break;                           
        }
    }

    public void EndEvent()
    {
        Destroy(eventPanel);
    }
    #endregion
 }