using UnityEngine;

public class C_AutoLogin : MonoBehaviour
{
    [SerializeField] private ClickEvent clickEvent;
    
    private void OnEnable()
    {
        if(!PlayerPrefs.HasKey(ConstData.KEY_LOGIN_TYPE)) return;
        int loginType = PlayerPrefs.GetInt(ConstData.KEY_LOGIN_TYPE);
        clickEvent.OnClick?.Invoke(loginType);
    }
}