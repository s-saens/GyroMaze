using UnityEngine;
using Newtonsoft.Json;

public class C_GravityRotater : MonoBehaviour
{
    public Event gyroEvent;
    public Transform lightTransform;


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
        Gyroscope gyro = JsonConvert.DeserializeObject<Gyroscope>(param, JsonSettings.Settings);
        
        // Set Gravity
        Vector3 gravityVector = new Vector3(gyro.gravity.x, gyro.gravity.z, gyro.gravity.y);
        Physics.gravity = gravityVector * 12 * Time.deltaTime * 60;

        // Set Light Rotation
        Quaternion lookAtRotation = Quaternion.LookRotation(gravityVector);
        lightTransform.rotation = lookAtRotation;
    }
}