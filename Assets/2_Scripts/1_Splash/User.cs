using UnityEngine;
using System;
using Firebase.Auth;
using Newtonsoft.Json;

public class User
{
    public int stage;
    public string countEndDate;

    [JsonIgnore] public TimeSpan remainTime
    {
        get
        {
            if(countEndDate == null) return new TimeSpan(-1, 0, 0);
            return DateTime.Parse(countEndDate) - DateTime.Now;
        }
    }

    private void Set(User user)
    {
        this.stage = user.stage;
        this.countEndDate = user.countEndDate;
    }

    public void SetStage(int value, bool prefs = true)
    {
        this.stage = value;
        if (prefs) PlayerPrefs.SetInt(KeyData.USER_STAGE, value);

        SaveToDB();
    }
    public void SetCount(string endDate, bool prefs = true)
    {
        this.countEndDate = endDate;
        if (prefs)
        {
            PlayerPrefs.SetString(KeyData.USER_COUNT_END_DATE, endDate);
        }

        SaveToDB();
    }

    public void LoadPrefs()
    {
        Debug.Log("LoadPrefs");
        this.stage = PlayerPrefs.GetInt(KeyData.USER_STAGE, 1);
        this.countEndDate = PlayerPrefs.GetString(KeyData.USER_COUNT_END_DATE, "7/18/2024 10:12:25 AM");
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
        Debug.Log(UserData.authUser.uid);
        if(UserData.authUser.uid == null) return;

        Debug.Log("LoadFromDB");
        FirebaseDBAccessor.GetValue<User>(
            FirebaseDBReference.user,
            (user) => {
                if(user.stage < UserData.databaseUser.stage) SaveToDB();
                else Set(user);
            }
        );
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
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