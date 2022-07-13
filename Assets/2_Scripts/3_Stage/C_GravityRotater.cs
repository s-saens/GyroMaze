using UnityEngine;
using Newtonsoft.Json;

public class C_GravityRotater : MonoBehaviour
{
    public Event gyroEvent;
    public Transform lightTransform;
    private Gyroscope gyro;


    public void OnEnable()
    {
        gyroEvent.callback += OnGyroAttitude;
    }

    public void OnDisable()
    {
        gyroEvent.callback -= OnGyroAttitude;
    }


    private void OnGyroAttitude(string param) // Gyroscope
    {
        // Set Gravity
        Vector3 gravityVector = JsonConvert.DeserializeObject<Vector3>(param, JsonSettings.Settings);
        Physics.gravity = gravityVector * 12 * Time.deltaTime * 60;

        // Set Light Rotation
        Quaternion lookAtRotation = Quaternion.LookRotation(gravityVector);
        lightTransform.rotation = lookAtRotation;
    }
}