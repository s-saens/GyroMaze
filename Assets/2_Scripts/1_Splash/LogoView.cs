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
            image.color = Color.Lerp(image.color, new Color(1, 1, 1, 1), 0.2f);
            yield return 0;
        }
        yield return new WaitForSecondsRealtime(0.5f);
        Login();
    }
    private void Login()
    {
        string loginType;
        if (NetworkChecker.isConnected && PlayerPrefs.HasKey(KeyData.LOGIN_TYPE)) loginType = PlayerPrefs.GetString(KeyData.LOGIN_TYPE);
        else loginType = "Offline";
#if UNITY_EDITOR
        loginType = "Test";
#endif
        loginEvent.Invoke(loginType);
    }

    private void OnLoginEnd(string param)
    {
        UserData.databaseUser.LoadFromDB();
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
        if(PlayerPrefs.HasKey(KeyData.LAST_POSITION))
        {
            int lastStage = PlayerPrefs.GetInt(KeyData.LAST_STAGE);
            GameData.stageIndex.value = lastStage;
            if(lastStage >= 0 && NetworkChecker.isConnected) whereToGo = SceneEnum.Stage;
        }
        
        SceneController.Instance.LoadScene(whereToGo, false);
    }
}