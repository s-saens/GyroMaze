using UnityEngine;
using System;
using Firebase.Auth;
using Newtonsoft.Json;

public class User
{
    public int stage;
    public string countEndDate;

    [JsonIgnore] public DateTime CountEndDate
    {
        get {
            return DateTime.Parse(countEndDate);
        }
    }

    [JsonIgnore] public TimeSpan remainTime
    {
        get {
            if(countEndDate == null) return new TimeSpan(-1, 0, 0);
            return CountEndDate - DateTime.Now;
        }
    }

    private void Set(User user)
    {
        this.stage = user.stage;
        this.countEndDate = user.countEndDate;
    }

    public void SetStage(int stage, bool prefs = true)
    {
        this.stage = stage;
        if (prefs) PlayerPrefs.SetInt(KeyData.USER_STAGE, stage);

        SaveToDB();
    }

    public void SetCount(string countEndDate, bool prefs = true)
    {
        this.countEndDate = countEndDate;
        if (prefs)
        {
            PlayerPrefs.SetString(KeyData.USER_COUNT_END_DATE, countEndDate);
        }

        SaveToDB();
    }

    public void LoadPrefs()
    {
        Debug.Log("LoadPrefs");
        this.stage = PlayerPrefs.GetInt(KeyData.USER_STAGE, 1);
        this.countEndDate = PlayerPrefs.GetString(KeyData.USER_COUNT_END_DATE, "1999/5/26 17:23:51");
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
                Debug.Log(user.CountEndDate + ", " + CountEndDate + " : " + (user.CountEndDate > CountEndDate));
                if (user.stage > stage) SetStage(user.stage);
                if (user.CountEndDate > CountEndDate) SetCount(user.countEndDate);
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