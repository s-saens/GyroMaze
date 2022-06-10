using UnityEngine;
using Firebase.Auth;
using Firebase.Database;

public class FirebaseInit
{
    public void Init()
    {
        InitInstances();
        InitDBRefs();
    }

    private void InitInstances()
    {
        FirebaseInstances.auth = FirebaseAuth.DefaultInstance;
        FirebaseInstances.db = FirebaseDatabase.GetInstance("https://gyromaze-a8ee3-default-rtdb.asia-southeast1.firebasedatabase.app/");
    }

    private void InitDBRefs()
    {
        DBRef.user = FirebaseInstances.db.GetReference("user");
        DBRef.maze = FirebaseInstances.db.GetReference("maze");
    }
}