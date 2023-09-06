using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class PanelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator _animator;
    public AudioSource soundEffect;
    public event Action OnFinish;
    public void AnimationFinishedTrigger() => OnFinish?.Invoke();
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _animator.SetBool("isFading2", true);
        }

    }

    private void OnEnable()
    {
        OnFinish += changeScene;
    }
    private void OnDisable()
    {
        OnFinish -= changeScene;
    }
    public void changeScene()
    {
        _animator.SetBool("isFading2", false);
        SceneManager.LoadScene("MainMenu");
    }
}
