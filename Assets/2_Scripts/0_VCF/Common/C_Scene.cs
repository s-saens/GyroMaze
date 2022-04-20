using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneEnum
{
    Login = 0,
    Lobby,
    Level,
}

public class C_Scene : _SingletonMono<C_Scene>
{
    public void LoadScene(SceneEnum se)
    {
        Debug.Log("LOADING~");
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