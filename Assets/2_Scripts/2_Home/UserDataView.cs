using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class UserDataView : MonoBehaviour
{
    [SerializeField] private RawImage image;
    [SerializeField] private GameObject loginButton;
    [SerializeField] private GameObject photo;

    private void Start()
    {
        loginButton.SetActive(!UserData.loggedIn);
        photo.SetActive(UserData.loggedIn);

        if(UserData.loggedIn) SetProfileImage(UserData.authUser.imgUrl);
    }
    
    private IEnumerator getTextureCoroutine;
    private void SetProfileImage(Uri imgUrl)
    {
        if(imgUrl == null) return;

        if(getTextureCoroutine != null) StopCoroutine(getTextureCoroutine);

        getTextureCoroutine = GetTexture(imgUrl);
        StartCoroutine(getTextureCoroutine);
    }

    private IEnumerator GetTexture(Uri imgUrl)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imgUrl);
        yield return www.SendWebRequest();
        if(www.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            image.texture = texture;
        }
    }
}