using UnityEngine;
using System;

public abstract class Event<T> : ScriptableObject
{
    public Action<T> callback;
    
    public void Invoke(T param)
    {
#if UNITY_EDITOR
        Type t = typeof(T);
        Debug.Log($"Event{t.ToString()} was invoked");
#endif
        callback?.Invoke(param);
    }
}

// DEFAULT
[CreateAssetMenu(menuName = "Event/_Default", fileName="Event")]
public class Event : ScriptableObject
{
    public Action callback;

    public void Invoke()
    {
        callback?.Invoke();
    }
}