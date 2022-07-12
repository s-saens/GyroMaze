using UnityEngine;

public class HomeInit : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.SetInt(KeyData.LAST_STAGE, -1);
        PlayerPrefs.DeleteKey(KeyData.LAST_POSITION);
    }
}