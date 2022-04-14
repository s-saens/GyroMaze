using UnityEngine;
using Firebase.Auth;
// using Firebase.Database;

public class User
{

}

public static class UserData
{
    private static FirebaseAuth auth = FirebaseAuth.DefaultInstance;
    // private static FirebaseDatabase database = FirebaseDatabase.DefaultInstance;

    public static void SetCredential(string googleIdToken, string googleAccessToken)
    {
        Credential credential = GoogleAuthProvider.GetCredential(googleIdToken, googleAccessToken);
        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithCredentialAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }

    private static FirebaseUser user
    {
        get { return auth.CurrentUser; }
    }

    // private static DatabaseReference userRef // Database에서 user 오브젝트 가져오기
    // {
    //     get { return database.GetReference(user.UserId); }
    // }
}