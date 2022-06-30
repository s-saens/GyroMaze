using UnityEngine;
using System;
using System.Threading.Tasks;
using Firebase.Extensions;
using Firebase.Database;
using Firebase.Auth;
using Newtonsoft.Json;

public static class UserDBUpdater
{
    private static DatabaseReference userDataRef;

    public static void FetchUser(string uid)
    {
        userDataRef = FirebaseDBReference.Reference("user", uid);
    }

    public static void UpdateUser(Action callback)
    {
        FirebaseDBAccessor.GetValue(
            FirebaseDBReference.Reference(FirebaseDBReference.user, UserData.uid.value),
            (value) => {
                User user = JsonConvert.DeserializeObject<User>(value);
                UserData.SetUser(user);
                callback?.Invoke();
            },
            () => {

                Debug.LogWarning("Searching User From Database Failed");
                IndicatorController.Instance.HideIndicator();
            }
        );
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

            IndicatorController.Instance.HideIndicator();
        }).LogExceptionIfFaulted();
    }
}