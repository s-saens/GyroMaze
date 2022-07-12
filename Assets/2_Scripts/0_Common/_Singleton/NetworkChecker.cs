using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

public class NetworkChecker : SingletonMono<NetworkChecker>
{
    public void Check(Action<bool> callback)
    {
        callback?.Invoke(!(Application.internetReachability == NetworkReachability.NotReachable));
    }

    private IEnumerator CheckCoroutine(Action<bool> callback)
    {
        UnityWebRequest req = UnityWebRequest.Get("https://naver.com");
        req.timeout = 30;
        yield return req.SendWebRequest();
        
        callback?.Invoke(req.result == UnityWebRequest.Result.Success);
    }
}