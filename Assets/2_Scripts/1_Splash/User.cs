using UnityEngine;
using System;
using Firebase.Auth;

public class PlayTime
{
    public string startDate;
    public int duration; // minute

    public PlayTime()
    {
        this.startDate = "SDS";
        this.duration = 10;
    }
}
public class Snapshot
{
    public int stage;
    public Vector3 position;

    public Snapshot()
    {
        this.stage = -1;
        this.position = new Vector3(0.5f, 0.5f, 0.5f);
    }
}

public class User
{
    public int stage;
    public PlayTime playTime;
    public Snapshot snapshot;

    public User()
    {
        stage = 1;
        playTime = new PlayTime();
        snapshot = new Snapshot();
    }

    public void SavePrefs()
    {
        PlayerPrefsExt.SetObject<User>(ConstData.KEY_USER, this);
    }
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