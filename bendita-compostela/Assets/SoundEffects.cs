using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SoundEffects : MonoBehaviour
{
    public List<AudioSource> soundEffects;
    public Button button;
    public Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() => soundEffects[0].Play());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            soundEffects[1].Play();
            _animator.SetBool("isFading2", true);
            StartCoroutine(WaitForSeconds());
        }
    }
    IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainMenu");
    }

}
