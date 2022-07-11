using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class LogoView : MonoBehaviour
{
    [SerializeField] private Event loginEvent;
    [SerializeField] private Event loginEndEvent;

    private Image image;

    private void Start()
    {
        image = this.GetComponent<Image>();
        StartCoroutine(FadeIn());
    }

    private void OnEnable()
    {
        loginEndEvent.callback += OnLoginEnd;
    }

    private void OnDisable()
    {
        loginEndEvent.callback -= OnLoginEnd;
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        while(image.color.a < 0.999)
        {
            image.color = Color.Lerp(image.color, new Color(1, 1, 1, 1), 0.5f);
            yield return 0;
        }
        yield return new WaitForSecondsRealtime(0.5f);
        Login();
    }
    private void Login()
    {
        NetworkChecker.Instance.Check((isConnected)=>{
            if(isConnected && PlayerPrefs.HasKey(KeyData.LOGIN_TYPE))
            {
                string loginType = PlayerPrefs.GetString(KeyData.LOGIN_TYPE);
                loginEvent.callback?.Invoke(loginType);
            }
            else
            {
                UserData.databaseUser.LoadPrefs();
                OnLoginEnd("");
            }
        });
    }

    private void OnLoginEnd(string param)
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        while (image.color.a > 0.0001)
        {
            image.color = Color.Lerp(image.color, new Color(1, 1, 1, 0), 0.2f);
            yield return 0;
        }
        LoadScene();
    }

    private void LoadScene()
    {
        SceneEnum whereToGo = SceneEnum.Home;
        if(PlayerPrefs.HasKey(KeyData.LAST_STAGE))
        {
            int lastStage = PlayerPrefs.GetInt(KeyData.LAST_STAGE);
            GameData.stageIndex.value = lastStage;
            if(lastStage >= 0) whereToGo = SceneEnum.Stage;
        }
        
        SceneController.Instance.LoadScene(whereToGo, false);
    }
}