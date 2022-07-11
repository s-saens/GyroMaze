using UnityEngine;
using System;
using Firebase.Auth;

public static class UserData
{
    public static bool loggedIn = false;

    // From Database
    public static User databaseUser = new User();
    // From Auth
    public static AuthUser authUser = new AuthUser();
}