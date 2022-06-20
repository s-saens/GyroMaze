using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroTest : MonoBehaviour
{
    public GyroEvent gyroEvent;
    public Transform cam;

    Quaternion rot = new Quaternion(0,0,1,0);

    public void OnEnable()
    {
        gyroEvent.OnGyroAttitude += OnGyroAttitude;
    }

    public void OnDisable()
    {
        gyroEvent.OnGyroAttitude -= OnGyroAttitude;
    }

    private void OnGyroAttitude(Gyroscope gyro)
    {
        Quaternion camRotation = gyro.attitude * rot;
    }
}
