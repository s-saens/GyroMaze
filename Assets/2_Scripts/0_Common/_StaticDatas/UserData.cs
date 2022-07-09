using UnityEngine;
using System;
using Firebase.Auth;

public static class UserData
{
    // From Database
    public static User databaseUser = new User();
    // From Auth
    public static AuthUser authUser = new AuthUser();
}