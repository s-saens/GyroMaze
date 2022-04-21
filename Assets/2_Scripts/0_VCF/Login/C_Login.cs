using UnityEngine;

using System.Threading.Tasks;
using Google;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using Newtonsoft.Json;

public class C_Login : MonoBehaviour
{
    // Events
    [SerializeField] private ClickEvent clickEvent;

    // Data
    [SerializeField] private UserData userData;

    // Login
    private int loginType = 0;
    public string clientId = "630965815426-go9stbqquhg1vsa37017ss6c1huqonub.apps.googleusercontent.com";
    private GoogleSignInConfiguration configuration;
    private FirebaseAuth authInstance;
    private FirebaseDatabase databaseInstance;

    private const string testAccountIdToken = "la6z9gjPwOZwKflp7ThN1amlJhk1";

    private void Awake()
    {
        authInstance = FirebaseAuth.DefaultInstance;
        databaseInstance = FirebaseDatabase.GetInstance("https://gyromaze-a8ee3-default-rtdb.asia-southeast1.firebasedatabase.app/");

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

    private void Login(int value)
    {
        C_Indicator.Instance.ShowIndicator();
        loginType = value;
        switch(value)
        {
            case 0:
                LoginGoogle();
                break;
            case 1:
                LoginGoogleGames();
                break;
            default:
                Debug.LogWarning($"Login Value {value} is not valid.");
                break;
        }
    }

    private void LoginGoogle()
    {
#if UNITY_EDITOR
        SetAuthCredential(testAccountIdToken, null);
#else
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;

        Debug.Log("Google Login...");

        GoogleSignIn.DefaultInstance.SignIn().ContinueWithOnMainThread(OnLoginFinished).LogExceptionIfFaulted();
#endif
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
            PlayerPrefs.SetInt(ConstData.KEY_LOGIN_TYPE, loginType);
            return;
        }
        C_Indicator.Instance.HideIndicator();
    }

    private void SetAuthCredential(string googleIdToken, string googleAccessToken)
    {
        Credential credential = GoogleAuthProvider.GetCredential(googleIdToken, googleAccessToken);

        Debug.Log("Setting Credetial");

        authInstance.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(task =>
        {
            if(task.IsCompleted)
            {
                SearchUserFromDatabase(task.Result);
                return;
            }
            Debug.LogWarning("Setting Credetial Failed");

            C_Indicator.Instance.HideIndicator();
        }).LogExceptionIfFaulted();
    }

    private void SearchUserFromDatabase(FirebaseUser fUser)
    {
        DatabaseReference userDataRef = databaseInstance.GetReference("user").Child(fUser.UserId);

        Debug.Log("Searching User From Database");

        userDataRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                if (task.Result.Value == null)
                {
                    AddUserOnDatabase(fUser, userDataRef);
                    return;
                }
                User user = JsonConvert.DeserializeObject<User>(task.Result.GetRawJsonValue());
                SetUserData(user, fUser);
                C_Scene.Instance.LoadScene(SceneEnum.Lobby);
                return;
            }

            Debug.LogWarning("Searching User From Database Failed");

            C_Indicator.Instance.HideIndicator();
        }).LogExceptionIfFaulted();
    }

    private void AddUserOnDatabase(FirebaseUser fUser, DatabaseReference userDataRef)
    {
        Debug.Log("Adding User On Database");

        User user = new User(fUser.DisplayName);
        string userJson = JsonConvert.SerializeObject(user);

        userDataRef.SetRawJsonValueAsync(userJson).ContinueWithOnMainThread(task =>
        {
            if(task.IsCompleted)
            {
                SetUserData(user, fUser);
                C_Scene.Instance.LoadScene(SceneEnum.Lobby);
                return;
            }
            Debug.LogWarning("Adding User On Database Failed");
        }).LogExceptionIfFaulted();
    }

    private void SetUserData(User user, FirebaseUser fUser)
    {
        userData.Set(user, fUser);
    }
}