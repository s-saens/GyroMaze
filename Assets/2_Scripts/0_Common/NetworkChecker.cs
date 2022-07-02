using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

public class NetworkChecker : MonoBehaviour
{
    private IEnumerator checkCoroutine;
    
    public void Check(Action<bool> callback)
    {
        if(checkCoroutine != null) StopCoroutine(checkCoroutine);
        checkCoroutine = CheckCoroutine(callback);

        StartCoroutine(checkCoroutine);
    }

    private IEnumerator CheckCoroutine(Action<bool> callback)
    {
        UnityWebRequest req = UnityWebRequest.Get("https://www.google.com");
        yield return req.SendWebRequest();

        if(req.result == UnityWebRequest.Result.Success) callback(true);
        else callback(false);
    }
}