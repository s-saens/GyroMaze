using UnityEngine;
using System;
using Firebase.Auth;

public static class UserData
{
    public static bool loggedIn = false;
    public static User databaseUser = new User();
    public static AuthUser authUser = new AuthUser();
}