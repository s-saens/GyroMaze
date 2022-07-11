using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

public class NetworkChecker : SingletonMono<NetworkChecker>
{
    public void Check(Action<bool> callback)
    {
        StartCoroutine(CheckCoroutine(callback));
    }

    private IEnumerator CheckCoroutine(Action<bool> callback)
    {
        UnityWebRequest req = UnityWebRequest.Get("https://www.google.com");
        req.timeout = 2;
        yield return req.SendWebRequest();
        
        callback?.Invoke(req.result == UnityWebRequest.Result.Success);
    }
}