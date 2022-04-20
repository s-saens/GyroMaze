using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class UserDataView : MonoBehaviour
{
    [SerializeField] private UserData userData;
    [SerializeField] private TMP_Text userNameText;
    [SerializeField] private RawImage image;

    private void Start()
    {
        SetNameText(userData.displayName.value);
        SetProfileImage(userData.imgUrl.value);
    }
    private void OnEnable()
    {
        userData.displayName.onChange += SetNameText;
        userData.imgUrl.onChange += SetProfileImage;
    }
    private void OnDisable()
    {
        userData.displayName.onChange -= SetNameText;
        userData.imgUrl.onChange -= SetProfileImage;
    }

    private void SetNameText(string name)
    {
        userNameText.text = name;
    }

    private IEnumerator getTextureCoroutine;
    private void SetProfileImage(Uri imgUrl)
    {
        if(getTextureCoroutine != null)
        {
            StopCoroutine(getTextureCoroutine);
        }
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