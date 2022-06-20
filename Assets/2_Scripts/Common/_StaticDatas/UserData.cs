using UnityEngine;
using System;
using Firebase.Auth;

public class User // Database 데이터
{
    public int stage;

    public User()
    {
        stage = 1;
    }
}

public static class UserData
{
    // From Database : User
    public static Data<int> stage = new Data<int>();

    // From Auth : FirebaseUser    
    public static Data<string> uid = new Data<string>();
    public static Data<string> displayName = new Data<string>();
    public static Data<Uri> imgUrl = new Data<Uri>();

    public static void SetUser(User user)
    {
        stage.value = user.stage;
    }
    public static void SetFirebaseUser(FirebaseUser fUser)
    {
        uid.value = fUser.UserId;
        displayName.value = fUser.DisplayName;
        imgUrl.value = fUser.PhotoUrl;
    }
    public static void SetFirebaseUser_Test(string u, string d)
    {
        uid.value = u;
        displayName.value = d;
        imgUrl.value = null;
    }
}