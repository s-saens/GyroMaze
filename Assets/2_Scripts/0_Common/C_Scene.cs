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

public class C_Scene : SingletonMono<C_Scene>
{
    public void LoadScene(SceneEnum se, bool indicator = true)
    {
        Debug.Log("LOADING~");
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync((int)se, LoadSceneMode.Single);
        IEnumerator sceneMove = SceneMoveCoroutine(loadOperation, indicator);
        StartCoroutine(sceneMove);
    }

    IEnumerator SceneMoveCoroutine(AsyncOperation loadOperation, bool indicator = true)
    {
        C_Indicator.Instance.ShowIndicator();
        while(!loadOperation.isDone)
        {
            yield return 0;
        }
    }
}