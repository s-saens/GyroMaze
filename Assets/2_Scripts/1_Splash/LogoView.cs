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
            string loginType = isConnected && PlayerPrefs.HasKey(ConstData.KEY_LOGIN_TYPE)
                                ? PlayerPrefs.GetString(ConstData.KEY_LOGIN_TYPE)
                                : "Offline";
            loginEvent.callback?.Invoke(loginType);
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
        SceneController.Instance.LoadScene(SceneEnum.Home, false);
    }
}