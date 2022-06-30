using UnityEngine;
using Firebase.Auth;
using Firebase.Database;

public class FirebaseInit
{
    public void Init()
    {
        InitInstances();
    }

    private void InitInstances()
    {
        FirebaseInstances.auth = FirebaseAuth.DefaultInstance;
        FirebaseInstances.db = FirebaseDatabase.GetInstance("https://gyromaze-a8ee3-default-rtdb.asia-southeast1.firebasedatabase.app/");
    }
}