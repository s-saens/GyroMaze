using UnityEngine;

using System.Threading.Tasks;
using Google;
using Firebase.Auth;
using Firebase.Extensions;

public enum LoginType
{
    Google,
    GoogleGames,
}

public class C_Login : MonoBehaviour
{
    // Events
    [SerializeField] private Event loginEvent;

    // Login
    private string loginType = "Google";
    public string clientId = "630965815426-go9stbqquhg1vsa37017ss6c1huqonub.apps.googleusercontent.com";
    private GoogleSignInConfiguration configuration;

    private void Awake()
    {
        configuration = new GoogleSignInConfiguration
        {
            WebClientId = clientId,
            RequestIdToken = true,
        };
    }

    private void OnEnable()
    {
        loginEvent.callback += Login;
    }
    private void OnDisable()
    {
        loginEvent.callback += Login;
    }

    private void Login(string value)
    {
        loginType = value;
        PopupIndicator.Instance.Show();
        switch(value)
        {
            case "Google":
                LoginGoogle();
                break;
            case "GoogleGames":
                LoginGoogleGames();
                break;
            default:
                Debug.LogWarning($"Login Value {value} is not valid.");
                break;
        }
    }

    private void LoginGoogle()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        
#if UNITY_EDITOR
        Debug.Log("Test Login...");
        TestLogin("TESTACCOUNT");
#else
        Debug.Log("Google Login...");
        GoogleSignIn.DefaultInstance.SignIn().ContinueWithOnMainThread(OnLoginFinished).LogExceptionIfFaulted();
#endif
    }

    private void TestLogin(string uid)
    {
        UserData.authUser.Set(uid, "TestAccount");
        UserDBUpdater.UpdateUser(OnLoginFinished);
    }

    private void LoginGoogleGames()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = true;
        GoogleSignIn.Configuration.RequestIdToken = false;

        Debug.Log("Google Games Login...");

        GoogleSignIn.DefaultInstance.SignIn().ContinueWithOnMainThread(OnSignInFinished).HandleFaulted();
    }

    private void OnSignInSilently()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;

        Debug.Log("Google Login (silently)...");

        GoogleSignIn.DefaultInstance.SignInSilently().ContinueWithOnMainThread(OnSignInFinished).HandleFaulted();
    }

    private void OnSignInFinished(Task<GoogleSignInUser> task)
    {
        if(task.IsCompleted)
        {
            PlayerPrefs.SetString(ConstData.KEY_LOGIN_TYPE, loginType);
            SetAuthCredential(task.Result.IdToken, null);
            return;
        }
        Debug.LogWarning("Login Failed");

        PopupIndicator.Instance.Hide();
    }

    private void SetAuthCredential(string googleIdToken, string googleAccessToken)
    {
        Credential credential = GoogleAuthProvider.GetCredential(googleIdToken, googleAccessToken);

        Debug.Log($"Setting Credetial : {googleIdToken}");

        FirebaseInstances.auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(task =>
        {
            if(task.IsCompleted)
            {
                UserData.authUser.Set(task.Result);
                UserDBUpdater.UpdateUser(OnLoginFinished);
                return;
            }
            Debug.LogWarning("Setting Credetial Failed");

            PopupIndicator.Instance.Hide();
        }).HandleFaulted();
    }

    private void OnLoginFinished()
    {
        if(UserData.databaseUser.snapshot.stage < 0)
        {
            SceneController.Instance.LoadScene(SceneEnum.Home);
            return;
        }

        SceneController.Instance.LoadScene(SceneEnum.Stage);

    }
}