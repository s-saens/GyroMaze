using UnityEngine;

public class AutoLoginController : MonoBehaviour
{
    [SerializeField] private ClickEvent clickEvent;
    private void Start()
    {
        if(!PlayerPrefs.HasKey(ConstData.KEY_LOGIN_TYPE)) return;
        int loginType = PlayerPrefs.GetInt(ConstData.KEY_LOGIN_TYPE);
        clickEvent.OnClick?.Invoke(loginType);
    }
}