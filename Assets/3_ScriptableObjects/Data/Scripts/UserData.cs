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

[CreateAssetMenu(fileName = "UserData", menuName = "Data/UserData")]
public class UserData : ScriptableObject
{
    // From Database : User
    public Data<string> displayName = new Data<string>();
    public Data<int> chapter = new Data<int>();
    public Data<int> stage = new Data<int>();

    // From Auth : FurebaseUser
    public Data<Uri> imgUrl = new Data<Uri>();

    public void Set(User user, FirebaseUser fUser)
    {
        this.displayName.value = user.name;
        this.chapter.value = user.chapter;
        this.stage.value = user.stage;

        this.imgUrl.value = fUser.PhotoUrl;
    }
}