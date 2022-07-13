using UnityEngine;
using System;
using Firebase.Auth;

public class User
{
    public int stage { get; private set; }
    public string countStartDate { get; private set; }
    public int countDuration { get; private set; }

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
        this.stage = PlayerPrefs.GetInt(KeyData.USER_STAGE, 1);
        this.countStartDate = PlayerPrefs.GetString(KeyData.USER_COUNT_START_DATE, "ASD");
        this.countDuration = PlayerPrefs.GetInt(KeyData.USER_COUNT_DURATION, 10);
    }

    public void SaveToDB()
    {
        FirebaseDBAccessor.SetValue<User>(FirebaseDBReference.user, this);
    }

    public void LoadFromDB()
    {
        FirebaseDBAccessor.GetValue<User>(
            FirebaseDBReference.user,
            (user) => Set(user)
        );
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