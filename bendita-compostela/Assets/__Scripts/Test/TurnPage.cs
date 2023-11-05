using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TurnPage: MonoBehaviour
{
    // Start is called before the first frame update
    public Animator _animator;

    public Image currentImage;
    public GameObject buttonObject;
    public GameObject highlight;

    [SerializeField]
    public List<Image> pages;
    public Button button;
    public int nextPage;

    void Start()
    {
    
        nextPage = 0;
    }

    // Update is called once per frame
    void Update()
    {
      

        if (nextPage == 4)
        {
            Debug.Log("hola");
            buttonObject.SetActive(false);
            highlight.SetActive(false);
            StartCoroutine(FinalTransition());
        }
    }
    public void getNextPage()
    {
        currentImage.sprite = pages[nextPage].sprite;
        nextPage++;

        ////////////////////////////////////////////// MUSICA /////////////////////////////////////
        MusicManager.Instance.CambiarParametro("cinematica",nextPage);
    }

    private IEnumerator FinalTransition()
    {
        yield return new WaitForSeconds(7);
        _animator.SetBool("isFading", true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainMenu");
    }
    
}
