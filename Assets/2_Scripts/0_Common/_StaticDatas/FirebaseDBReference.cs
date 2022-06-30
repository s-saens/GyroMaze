using UnityEngine;
using Firebase.Database;

public static class FirebaseDBReference
{
    public static readonly string maze = "maze";
    public static readonly string user = "user";
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
}