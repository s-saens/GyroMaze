using UnityEngine;
using System.Collections;

public class GyroDebug : MonoBehaviour
{
    public GyroEvent gyroEvent;
    public Transform cam;

    private string text = "";

    public void OnEnable()
    {
        gyroEvent.OnGyroAttitude += OnGyroAttitude;
    }

    public void OnDisable()
    {
        gyroEvent.OnGyroAttitude -= OnGyroAttitude;
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, h*2/100, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = new Color(1f, 1f, 1f, 1.0f);

        GUI.Label(rect, text, style);
    }

    private void OnGyroAttitude(Gyroscope gyro)
    {
        text = $"GyroRate : {gyro.rotationRateUnbiased}";
        text += $"\nGravity : {gyro.gravity}";
        text += $"\nGravity : {Physics.gravity}";
    }
}