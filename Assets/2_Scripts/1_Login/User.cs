using UnityEngine;
using System;
using Firebase.Auth;

public class PlayTime
{
    public int startDate;
    public int duration;
}

public class User
{
    public int stage = 4;
    public PlayTime playTime;
}

public class AuthUser
{
    public string uid;
    public string displayName;
    public Uri imgUrl;

    public void Set(FirebaseUser fUser)
    {
        this.uid = fUser.UserId;
        this.displayName = fUser.DisplayName;
        this.imgUrl = fUser.PhotoUrl;
    }
    public void Set(string uid, string displayName)
    {
        this.uid = uid;
        this.displayName = displayName;
    }
}