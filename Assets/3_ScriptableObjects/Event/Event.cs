using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Event")]
public class Event : ScriptableObject
{
    public Action<string> callback;

    public void Invoke(string parameter)
    {
        callback?.Invoke(parameter);
    }
}