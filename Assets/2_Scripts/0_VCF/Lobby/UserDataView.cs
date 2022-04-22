using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class UserDataView : MonoBehaviour
{
    [SerializeField] private TMP_Text userNameText;
    [SerializeField] private RawImage image;

    private void Start()
    {
        SetNameText(UserData.displayName.value);
        SetProfileImage(UserData.imgUrl.value);
    }
    private void OnEnable()
    {
        UserData.displayName.onChange += SetNameText;
        UserData.imgUrl.onChange += SetProfileImage;
    }
    private void OnDisable()
    {
        UserData.displayName.onChange -= SetNameText;
        UserData.imgUrl.onChange -= SetProfileImage;
    }

    private void SetNameText(string name)
    {
        userNameText.text = name;
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