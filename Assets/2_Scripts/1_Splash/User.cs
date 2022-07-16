using UnityEngine;
using System;
using Firebase.Auth;

public class User
{
    public int stage;
    public string countStartDate;
    public int countDuration;

    private void Set(User user)
    {
        this.stage = user.stage;
        this.countStartDate = user.countStartDate;
        this.countDuration = user.countDuration;
    }

    public void SetStage(int value, bool prefs = true)
    {
        this.stage = value;
        if (prefs) PlayerPrefs.SetInt(KeyData.USER_STAGE, value);

        SaveToDB();
    }
    public void SetCount(string startDate, int duration, bool prefs = true)
    {
        this.countStartDate = startDate;
        this.countDuration = duration;
        if (prefs)
        {
            PlayerPrefs.SetString(KeyData.USER_COUNT_START_DATE, startDate);
            PlayerPrefs.SetInt(KeyData.USER_COUNT_DURATION, duration);
        }

        SaveToDB();
    }

    public void LoadPrefs()
    {
        Debug.Log("LoadPrefs");
        this.stage = PlayerPrefs.GetInt(KeyData.USER_STAGE, 1);
        this.countStartDate = PlayerPrefs.GetString(KeyData.USER_COUNT_START_DATE, "ASD");
        this.countDuration = PlayerPrefs.GetInt(KeyData.USER_COUNT_DURATION, 10);
    }

    public void SaveToDB()
    {
        Debug.Log("SaveToDB");
        if (UserData.loggedIn && NetworkChecker.isConnected)
        {
            FirebaseDBAccessor.SetValue<User>(FirebaseDBReference.user, this);
        }
    }

    public void LoadFromDB()
    {
        Debug.Log("LoadFromDB");
        FirebaseDBAccessor.GetValue<User>(
            FirebaseDBReference.user,
            (user) => {
                if(user.stage < UserData.databaseUser.stage) SaveToDB();
                else Set(user);
            }
        );
    }
}

public class AuthUser
{
    public string uid
    {
        get {
            return PlayerPrefs.GetString(KeyData.USER_UID, null);
        }
    }
    public Uri imgUrl;

    public void Set(FirebaseUser fUser)
    {
        PlayerPrefs.SetString(KeyData.USER_UID, fUser.UserId);
        this.imgUrl = fUser.PhotoUrl;
    }
    public void Set(string uid)
    {
        PlayerPrefs.SetString(KeyData.USER_UID, uid);
    }
}