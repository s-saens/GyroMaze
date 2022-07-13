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
    [SerializeField] private Event loginEndEvent;

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
        loginEvent.callback -= Login;
    }

    private void Login(string value)
    {
        loginType = value;

        switch(value)
        {
            case "Test":
                LoginTest();
                break;
            case "Offline":
                LoginOffline();
                break;
            case "Google":
                LoginGoogle();
                break;
            case "GoogleGames":
                LoginGoogleGames();
                break;
            case "GoogleSilently":
                LoginGoogleSilently();
                break;
            default:
                Debug.LogWarning($"Login Value {value} is not valid.");
                break;
        }
    }

    private void LoginTest()
    {
        UserData.authUser.Set("TESTACCOUNT", "TestAccountName");
        LoginOffline();
    }

    private void LoginOffline()
    {
        LoginEnd();
    }

    private void LoginGoogle()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        
        Debug.Log("Google Login...");
        GoogleSignIn.DefaultInstance.SignIn().ContinueWithOnMainThread(OnSignInFinished).HandleFaulted();
    }


    private void LoginGoogleGames()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = true;
        GoogleSignIn.Configuration.RequestIdToken = false;

        Debug.Log("Google Games Login...");

        GoogleSignIn.DefaultInstance.SignIn().ContinueWithOnMainThread(OnSignInFinished).HandleFaulted();
    }

    private void LoginGoogleSilently()
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
            PlayerPrefs.SetString(KeyData.LOGIN_TYPE, loginType);
            SetAuthCredential(task.Result.IdToken, null);
            return;
        }
        Debug.LogWarning("Login Failed");
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
                UserData.loggedIn = true;
                LoginEnd();
                return;
            }
            Debug.LogWarning("Setting Credetial Failed");
        }).HandleFaulted();
    }

    private void LoginEnd()
    {
        UserData.databaseUser.LoadPrefs();
        UserData.databaseUser.SaveToDB();

        loginEndEvent.callback.Invoke("");
    }
}