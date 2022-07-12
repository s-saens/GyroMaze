using UnityEngine;
using Firebase.Database;

public static class FirebaseDBReference
{
    public static readonly string maze = "maze";
    public static DatabaseReference user
    {
        get {
            return Reference("user", UserData.authUser.uid);
        }
    }

    public static DatabaseReference Reference(params string[] keys)
    {
        DatabaseReference reference = FirebaseInstances.db.GetReference(keys[0]);
        int len = keys.Length;
        for (int i = 1; i < len; ++i)
        {
            reference = reference.Child(keys[i]);
        }

        return reference;
    }
    
    public static DatabaseReference Reference(DatabaseReference dbRef, params string[] keys)
    {
        int len = keys.Length;
        for (int i = 0; i < len; ++i)
        {
            dbRef = dbRef.Child(keys[i]);
        }

        return dbRef;
    }
}