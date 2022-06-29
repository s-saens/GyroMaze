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
    public void LoadScene(SceneEnum se, bool indicator = true)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync((int)se, LoadSceneMode.Single);
        IEnumerator sceneMove = SceneMoveCoroutine(loadOperation, indicator);
        StartCoroutine(sceneMove);
    }

    IEnumerator SceneMoveCoroutine(AsyncOperation loadOperation, bool indicator = true)
    {
        IndicatorController.Instance.ShowIndicator();
        while(!loadOperation.isDone)
        {
            yield return 0;
        }
    }
}