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

    //Sound assets
    [SerializeField] List<AudioClip> audios;



    void Start()
    {
        SoundManager.Instance.PlayMusic(audios[0], 1f);
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

    }

    private IEnumerator FinalTransition()
    {
        yield return new WaitForSeconds(3);
        _animator.SetBool("isFading", true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainMenu");
    }
}
