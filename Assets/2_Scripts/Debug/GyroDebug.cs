using UnityEngine;
using System.Collections;

public class GyroDebug : MonoBehaviour
{
    private Gyroscope gyro;
    private bool gyroEnabled;

    private void Start()
    {
        gyroEnabled = SystemInfo.supportsGyroscope;
        if(gyroEnabled == false)
        {
            return;
        }

        gyro = Input.gyro;
        gyro.enabled = true;
    }
    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, h*2/100, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = new Color(1f, 1f, 1f, 1.0f);

        string text = "";
        if(gyroEnabled == true)
        {
            text = $"Gyroscope : {gyro.rotationRate}";
        }
        else
        {
            text = $"Gyroscope is not supported";
        }
        GUI.Label(rect, text, style);
    }
}