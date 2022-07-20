using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

public static class NetworkChecker
{
    public static bool isConnected
    {
        get {
            return !(Application.internetReachability == NetworkReachability.NotReachable);
        }
    }
}