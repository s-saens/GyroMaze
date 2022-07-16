using UnityEngine;
using System;
using Firebase.Auth;

public static class UserData
{
    public static bool loggedIn
    {
        get {
            return PlayerPrefs.HasKey(KeyData.USER_UID);
        }
    }
    public static User databaseUser = new User();
    public static AuthUser authUser = new AuthUser();
}