using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

public class NetworkChecker : SingletonMono<NetworkChecker>
{
    public static Data<bool> isConnected = new Data<bool>();

    private void Start()
    {
        StartCoroutine(CheckCoroutine());

        isConnected.onChange += (v)=> Debug.Log(v);
    }

    private IEnumerator CheckCoroutine()
    {
        UnityWebRequest req = UnityWebRequest.Get("https://www.google.com");
        req.timeout = 3;
        yield return req.SendWebRequest();

        isConnected.value = req.result == UnityWebRequest.Result.Success;
        yield return new WaitForSecondsRealtime(1);
        yield return CheckCoroutine(); // 무한히 실행
    }
}