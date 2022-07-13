using UnityEngine;
using System;
using Firebase.Database;
using Firebase.Extensions;
using Newtonsoft.Json;


public static class FirebaseDBAccessor
{
    private static JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
    public static void GetValue<T>(DatabaseReference dbRef, Action<T> callback = null, Action noDataCallback = null)
    {
        dbRef.GetValueAsync().ContinueWithOnMainThread((task)=> {
            if (task.IsCompleted)
            {
                if (task.Result.Value == null)
                {
                    noDataCallback?.Invoke();
                    return;
                }
                serializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                callback?.Invoke(JsonConvert.DeserializeObject<T>(task.Result.GetRawJsonValue(), serializerSettings));
                return;
            }
        }).HandleFaulted();
    }

    public static void SetValue<T>(DatabaseReference dbRef, T value, Action callback = null)
    {
        string json = JsonConvert.SerializeObject(value);
        dbRef.SetRawJsonValueAsync(json).ContinueWithOnMainThread((task) =>
        {
            if (task.IsCompleted)
            {
                callback?.Invoke();
            }
        }).HandleFaulted();
    }
}