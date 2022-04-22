using UnityEngine;
using System;
using Firebase.Auth;

public class User
{
    public string name;
    public int chapter;
    public int stage;

    public User(string n)
    {
        name = n;
        chapter = 1;
        stage = 1;
    }
}

public static class UserData
{
    // From Database : User
    public static Data<string> displayName = new Data<string>();
    public static Data<int> chapter = new Data<int>();
    public static Data<int> stage = new Data<int>();

    // From Auth : FurebaseUser
    public static Data<Uri> imgUrl = new Data<Uri>();

    public static void Set(User user, FirebaseUser fUser)
    {
        displayName.value = user.name;
        chapter.value = user.chapter;
        stage.value = user.stage;

        imgUrl.value = fUser.PhotoUrl;
    }
}