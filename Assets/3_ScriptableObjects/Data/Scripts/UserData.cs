using UnityEngine;
using Firebase.Auth;
using Firebase.Database;

public static class UserData
{
    public static FirebaseAuth authInstance
    {
        get
        {
            return FirebaseAuth.DefaultInstance;
        }
    }

    public static FirebaseUser user
    {
        get
        {
            return authInstance.CurrentUser;
        }
    }

    public static FirebaseDatabase databaseInstance
    {
        get
        {
            return FirebaseDatabase.DefaultInstance;
        }
    }

    public static DatabaseReference userRef
    {
        get
        {
            return databaseInstance.GetReference(user.UserId);
        }
    }

    public static int chapter
    {
        get
        {
            return 0;
        }
    }
    public static int stage;

}