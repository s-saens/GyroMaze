using UnityEngine;
using System;
using System.Threading.Tasks;
using Firebase.Extensions;
using Firebase.Database;
using Firebase.Auth;
using Newtonsoft.Json;

public static class DatabaseUpdater
{
    private static DatabaseReference userDataRef;

    public static void FetchUser(string uid)
    {
        userDataRef = DBRef.user.Child(uid);
    }

    public static void UpdateUser(Action callback)
    {
        FetchUser(UserData.uid.value);

        userDataRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                if (task.Result.Value == null)
                {
                    AddUserOnDatabase(callback);
                    return;
                }

                User user = JsonConvert.DeserializeObject<User>(task.Result.GetRawJsonValue());
                UserData.SetUser(user);

                callback?.Invoke();
                return;
            }

            Debug.LogWarning("Searching User From Database Failed");

            C_Indicator.Instance.HideIndicator();
        }).LogExceptionIfFaulted();
    }

    private static void AddUserOnDatabase(Action callback)
    {
        Debug.Log("Adding User On Database");

        User user = new User();
        string userJson = JsonConvert.SerializeObject(user);

        userDataRef.SetRawJsonValueAsync(userJson).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                UserData.SetUser(user);
                callback?.Invoke();
                return;
            }

            Debug.LogWarning("Adding User On Database Failed");

            C_Indicator.Instance.HideIndicator();
        }).LogExceptionIfFaulted();
    }
}