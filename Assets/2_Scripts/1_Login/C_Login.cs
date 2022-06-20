using UnityEngine;

using System.Threading.Tasks;
using Google;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using Newtonsoft.Json;

public enum LoginType
{
    Google,
    GoogleGames,
}

public class C_Login : MonoBehaviour
{
    // Initiator
    FirebaseInit fi = new FirebaseInit();

    // Events
    [SerializeField] private ButtonEvent clickEvent;

    // Login
    private string loginType = "Google";
    public string clientId = "630965815426-go9stbqquhg1vsa37017ss6c1huqonub.apps.googleusercontent.com";
    private GoogleSignInConfiguration configuration;

    private void Awake()
    {
        fi.Init();
        configuration = new GoogleSignInConfiguration
        {
            WebClientId = clientId,
            RequestIdToken = true,
        };
    }

    private void OnEnable()
    {
        clickEvent.OnClick += Login;
    }
    private void OnDisable()
    {
        clickEvent.OnClick += Login;
    }

    private void Login(string value)
    {
        C_Indicator.Instance.ShowIndicator();
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
        UserData.SetFirebaseUser_Test(uid, "TestAccount");
        UserDBUpdater.UpdateUser(() =>
        {
            C_Scene.Instance.LoadScene(SceneEnum.Lobby);
        });
    }

    private void LoginGoogleGames()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = true;
        GoogleSignIn.Configuration.RequestIdToken = false;

        Debug.Log("Google Games Login...");

        GoogleSignIn.DefaultInstance.SignIn().ContinueWithOnMainThread(OnLoginFinished).LogExceptionIfFaulted();
    }

    private void OnSignInSilently()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;

        Debug.Log("Google Login (silently)...");

        GoogleSignIn.DefaultInstance.SignInSilently().ContinueWithOnMainThread(OnLoginFinished).LogExceptionIfFaulted();
    }

    private void OnLoginFinished(Task<GoogleSignInUser> task)
    {
        if(task.IsCompleted)
        {
            SetAuthCredential(task.Result.IdToken, null);
            PlayerPrefs.SetString(ConstData.KEY_LOGIN_TYPE, loginType);
            return;
        }
        Debug.LogWarning("Login Failed");

        C_Indicator.Instance.HideIndicator();
    }

    private void SetAuthCredential(string googleIdToken, string googleAccessToken)
    {
        Credential credential = GoogleAuthProvider.GetCredential(googleIdToken, googleAccessToken);

        Debug.Log($"Setting Credetial : {googleIdToken}");

        FirebaseInstances.auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(task =>
        {
            if(task.IsCompleted)
            {
                UserData.SetFirebaseUser(task.Result);

                UserDBUpdater.UpdateUser(() => {
                    C_Scene.Instance.LoadScene(SceneEnum.Lobby);
                });
                return;
            }
            Debug.LogWarning("Setting Credetial Failed");

            C_Indicator.Instance.HideIndicator();
        }).LogExceptionIfFaulted();
    }
}