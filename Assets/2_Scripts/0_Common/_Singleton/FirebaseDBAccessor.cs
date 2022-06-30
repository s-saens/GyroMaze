using UnityEngine;
using System;
using Firebase.Database;
using Firebase.Extensions;
using System.Threading.Tasks;


public static class FirebaseDBAccessor
{
    public static void GetValue(DatabaseReference dbRef, Action<string> callback = null, Action noDataCallback = null)
    {
        dbRef.GetValueAsync().ContinueWithOnMainThread((task)=> {
            if (task.IsCompleted)
            {
                if (task.Result.Value == null)
                {
                    Debug.LogError($"THERE IS NO DATA ON DATABASE");
                    noDataCallback?.Invoke();
                    return;
                }
                callback?.Invoke(task.Result.GetRawJsonValue());
            }
        }).LogExceptionIfFaulted();
    }

    public static void SetValue(DatabaseReference dbRef, string value, Action callback = null)
    {
        dbRef.SetRawJsonValueAsync(value).ContinueWithOnMainThread((task) =>
        {
            if (task.IsCompleted)
            {
                callback?.Invoke();
            }
        }).LogExceptionIfFaulted();
    }
}