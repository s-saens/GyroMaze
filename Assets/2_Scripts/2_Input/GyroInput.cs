using UnityEngine;

public class GyroInput : MonoBehaviour
{
    public GyroEvent gyroEvent;

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
        gyroEvent.OnGyroAttitude.Invoke(gyro);
    }
}