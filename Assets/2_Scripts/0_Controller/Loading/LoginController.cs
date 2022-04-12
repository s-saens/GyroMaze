// using UnityEngine;
// using UnityEngine.SceneManagement;
// using Firebase.Auth;

// using GooglePlayGames;
// using GooglePlayGames.BasicApi;
// using UnityEngine.SocialPlatforms;
// using System.Threading.Tasks;

// public class LoginController : MonoBehaviour
// {
//     [SerializeField] private ClickEvent clickEvent;

//     private FirebaseAuth auth = FirebaseAuth.DefaultInstance;
//     private string googleIdToken;
//     private string googleAccessToken;

//     private void OnEnable()
//     {
//         clickEvent.OnClick += Login;
//     }
//     private void OnDisable()
//     {
//         clickEvent.OnClick += Login;
//     }

//     private void Login(int value)
//     {
//         if(value == 0) LoginGoogle();
//     }


//     public void LoginGoogle()
//     {
//         Debug.Log("[SignIn] Successful!");
//         SceneManager.LoadScene(1);
//     }
// }