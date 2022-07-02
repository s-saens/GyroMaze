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
                Debug.Log($"No User of UID: {UserData.uid.value}. Adding new instance of user...");
                AddUserOnDatabase(() => {
                    IndicatorController.Instance.Hide();
                });
            }
        );
    }

    private static void AddUserOnDatabase(Action callback)
    {
        Debug.Log("Adding User On Database");

        User user = new User();
        string userJson = JsonConvert.SerializeObject(user);

        FirebaseDBAccessor.SetValue(
            FirebaseDBReference.Reference(FirebaseDBReference.user, UserData.uid.value),
            userJson,
            () => {
                callback?.Invoke();
            }
        );
    }
}