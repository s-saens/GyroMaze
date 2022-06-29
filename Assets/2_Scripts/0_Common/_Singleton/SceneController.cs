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
    [SerializeField] private Event sceneLoadEvent;

    private void OnEnable()
    {
        sceneLoadEvent.callback += LoadScene;

    }

    private void OnDisable()
    {
        sceneLoadEvent.callback -= LoadScene;
    }

    private void LoadScene(object param)
    {
        SceneEnum se = (SceneEnum)param;
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync((int)se, LoadSceneMode.Single);
        IEnumerator sceneMove = SceneMoveCoroutine(loadOperation);
        StartCoroutine(sceneMove);
    }

    private IEnumerator SceneMoveCoroutine(AsyncOperation loadOperation, bool indicator = true)
    {
        IndicatorController.Instance.ShowIndicator();
        while(!loadOperation.isDone)
        {
            yield return 0;
        }
    }
}