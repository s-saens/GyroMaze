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
            FirebaseDBReference.Reference(FirebaseDBReference.user, UserData.authUser.uid),
            (value) => {
                UserData.databaseUser = JsonConvert.DeserializeObject<User>(value);
                callback?.Invoke();
            },
            () => {
                Debug.Log($"No User of UID: {UserData.authUser.uid}. Adding new instance of user...");
                AddUserOnDatabase(() => {
                    PopupIndicator.Instance.Hide();
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
            FirebaseDBReference.Reference(FirebaseDBReference.user, UserData.authUser.uid),
            userJson,
            () => {
                callback?.Invoke();
            }
        );
    }
}