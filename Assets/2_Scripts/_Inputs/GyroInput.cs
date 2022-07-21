using UnityEngine;
using Newtonsoft.Json;

public class GyroInput : MonoBehaviour
{
    public EventGyroscope gyroEvent;

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

    private void FixedUpdate()
    {
        if(gyroEnabled == false) return;
        gyroEvent.Invoke(gyro);
    }
}