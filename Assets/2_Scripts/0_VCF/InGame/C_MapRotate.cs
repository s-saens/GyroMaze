using UnityEngine;

public class C_MapRotate : MonoBehaviour
{
    public GyroEvent gyroEvent;
    public Transform lightTransform;


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
        // Set Gravity
        Vector3 gravityVector = new Vector3(gyro.gravity.x, gyro.gravity.z, gyro.gravity.y);
        Physics.gravity = gravityVector * 12;

        // Set Light Rotation
        Quaternion lookAtRotation = Quaternion.LookRotation(gravityVector);
        lightTransform.rotation = lookAtRotation;
    }
}