using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicAnimation : MonoBehaviour
{
   
    public enum SceneReference
    {
        Frame1,
        Frame2,
        Frame3,
        None,
    }

    public static CinematicAnimation instance;
    private SceneReference _currentScene;

    private void Awake()
    {
        instance = this;
    }

    public void LoadScene(SceneReference toLoad)
    {
        
        if (toLoad == _currentScene) return;
        
        string from = _currentScene.ToString();
        string to = toLoad.ToString();

        AsyncOperation op = SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);
        op.completed += (_) =>
        {
            Scene s = SceneManager.GetSceneByName(from);
            if (s != null & s.IsValid()) SceneManager.UnloadSceneAsync(s);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(to));
        };

        _currentScene = toLoad;
    }
}
