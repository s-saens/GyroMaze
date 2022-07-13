using UnityEngine;
using Newtonsoft.Json;

public class GyroInput : MonoBehaviour
{
    public Event gyroEvent;

    private Gyroscope gyro;
    private bool gyroEnabled;

    private void Start()
    {
        gyroEnabled = SystemInfo.supportsGyroscope;
        if (gyroEnabled == false)
        {
            return;
        }

        gyro = Input.gyro;
        gyro.enabled = true;
    }

    private void Update()
    {
        if(gyroEnabled == false)
        {
            return;
        }

        Vector3 gravity = new Vector3(gyro.gravity.x, gyro.gravity.z, gyro.gravity.y);
        gyroEvent.Invoke(JsonConvert.SerializeObject(gravity, JsonSettings.Settings));
    }
}