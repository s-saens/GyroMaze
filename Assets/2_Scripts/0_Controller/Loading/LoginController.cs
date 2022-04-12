using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;

public class LoginController : MonoBehaviour
{
    [SerializeField] private ClickEvent clickEvent;

    private FirebaseAuth auth = FirebaseAuth.DefaultInstance;
    private string googleIdToken;
    private string googleAccessToken;

    private void OnEnable()
    {
        clickEvent.OnClick += Login;
    }
    private void OnDisable()
    {
        clickEvent.OnClick += Login;
    }

    private void Login(int value)
    {
        if(value == 0) LoginGoogle();
    }

    public void LoginGoogle()
    {
        Credential credential = GoogleAuthProvider.GetCredential(googleIdToken, googleAccessToken);
        auth.SignInWithCredentialAsync(credential).ContinueWith(task=>
        {
            if(task.IsCanceled) { Debug.LogError("[SignIn] Canceled"); return; }
            if(task.IsCanceled) { Debug.LogError("[SignIn] Error encountered"); return; }
        });

        Debug.Log("[SignIn] Successful!");
        SceneManager.LoadScene(1);
    }
}