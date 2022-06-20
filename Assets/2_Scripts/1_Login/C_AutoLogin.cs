using UnityEngine;

public class C_AutoLogin : MonoBehaviour
{
    [SerializeField] private ButtonEvent buttonEvent;
    
    private void OnEnable()
    {
        if(!PlayerPrefs.HasKey(ConstData.KEY_LOGIN_TYPE)) return;
        string loginType = PlayerPrefs.GetString(ConstData.KEY_LOGIN_TYPE);
        buttonEvent.OnClick?.Invoke(loginType);
    }
}