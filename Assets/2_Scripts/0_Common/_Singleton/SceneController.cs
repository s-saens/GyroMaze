using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneEnum
{
    Login = 0,
    Home,
    Level,
}

public class SceneController : SingletonMono<SceneController>
{
    [SerializeField] private Event loadSceneEvent;

    private void OnEnable()
    {
        loadSceneEvent.callback += LoadScene;
    }
    private void OnDisable()
    {
        loadSceneEvent.callback -= LoadScene;
    }

    private void LoadScene(string param)
    {
        string[] parameters = param.Split(',');

        SceneEnum se = (SceneEnum)(int.Parse(parameters[0]));
        bool indicator = true;
        
        if(parameters.Length > 1) indicator = bool.Parse(parameters[1]);

        LoadScene(se, indicator);
    }

    public void LoadScene(SceneEnum se, bool indicator = true)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync((int)se, LoadSceneMode.Single);
        IEnumerator sceneMove = SceneMoveCoroutine(loadOperation, indicator);
        StartCoroutine(sceneMove);
    }

    IEnumerator SceneMoveCoroutine(AsyncOperation loadOperation, bool indicator = true)
    {
        PopupIndicator.Instance.Show();
        while(!loadOperation.isDone)
        {
            yield return 0;
        }
    }
}