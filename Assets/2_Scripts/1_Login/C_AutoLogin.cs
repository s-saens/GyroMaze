using UnityEngine;

public class C_AutoLogin : MonoBehaviour
{
    [SerializeField] private Event buttonEvent;
    
    private void Start()
    {
        if(!PlayerPrefs.HasKey(ConstData.KEY_LOGIN_TYPE)) return;
        string loginType = PlayerPrefs.GetString(ConstData.KEY_LOGIN_TYPE, "Google");
        buttonEvent.callback?.Invoke(loginType);
    }
}