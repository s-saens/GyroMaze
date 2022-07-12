using UnityEngine;

public class HomeInit : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.DeleteKey(KeyData.LAST_POSITION);
    }
}