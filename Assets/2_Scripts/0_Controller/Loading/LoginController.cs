using UnityEngine;
using UnityEngine.SceneManagement;

using System.Collections.Generic;
using System.Threading.Tasks;
using Google;

public class LoginController : MonoBehaviour
{
    [SerializeField] private ClickEvent clickEvent;
    private GoogleSignInConfiguration configuration;
    private int loginType = 0;

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
        LoadingIndicatorController.Instance.ShowIndicator();
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
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;

        Debug.Log("Google Login...");

        GoogleSignIn.DefaultInstance.SignIn()
            .ContinueWith(OnLoginFinished);
    }

    private void LoginGoogleGames()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = true;
        GoogleSignIn.Configuration.RequestIdToken = false;

        Debug.Log("Google Games Login...");

        GoogleSignIn.DefaultInstance.SignIn()
            .ContinueWith(OnLoginFinished);
    }

    private void OnSignInSilently()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;

        Debug.Log("Google Login (silently)...");

        GoogleSignIn.DefaultInstance.SignInSilently()
            .ContinueWith(OnLoginFinished);
    }

    internal void OnLoginFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            using (IEnumerator<System.Exception> enumerator =
                    task.Exception.InnerExceptions.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    GoogleSignIn.SignInException error =
                            (GoogleSignIn.SignInException)enumerator.Current;
                    Debug.LogWarning("Got Error: " + error.Status + " " + error.Message);
                }
                else
                {
                    Debug.LogError("Got Unexpected Exception?!?" + task.Exception);
                }
            }
        }
        else if (task.IsCanceled)
        {
            Debug.Log("Canceld");
        }
        else
        {
            Debug.Log("Welcome: " + task.Result.DisplayName + "!");
            UserData.SetCredential(task.Result.IdToken, null);
            PlayerPrefs.SetInt(ConstData.KEY_LOGIN_TYPE, loginType);
            SceneManager.LoadScene(1);
        }
    }
}