using UnityEngine;
using Firebase.Auth;
using Firebase.Database;

public class FirebaseInitiator : SingletonMono<FirebaseInitiator>
{
    public void Awake()
    {
        InitInstances();
    }

    private void InitInstances()
    {
        FirebaseInstances.auth = FirebaseAuth.DefaultInstance;
        FirebaseInstances.db = FirebaseDatabase.GetInstance("https://gyromaze-a8ee3-default-rtdb.asia-southeast1.firebasedatabase.app/");
    }
}