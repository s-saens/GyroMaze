using UnityEngine;
using System;
using Newtonsoft.Json;

[CreateAssetMenu(menuName = "Event")]

public class Event : ScriptableObject
{
    public Action<object> callback;

    public void Invoke(object param)
    {
        callback?.Invoke(param);
    }
}