using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneEnum
{
    Loading = 0,
    Lobby,
    Level,
}

public class C_Scene : _SingletonMono<C_Scene>
{
    [SerializeField] private ClickEvent clickEvent;

    public void LoadScene(SceneEnum se)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync((int)se, LoadSceneMode.Single);
        IEnumerator sceneMove = SceneMoveCoroutine(loadOperation);
        StartCoroutine(sceneMove);
    }

    IEnumerator SceneMoveCoroutine(AsyncOperation loadOperation)
    {
        C_Indicator.Instance.ShowIndicator("Scene Load");
        while(!loadOperation.isDone)
        {
            yield return 0;
        }
        C_Indicator.Instance.HideIndicator("Scene Load");
    }
}