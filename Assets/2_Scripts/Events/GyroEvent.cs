using UnityEngine;
using System;

[CreateAssetMenu(fileName = "GyroEvent", menuName = "Events/GyroEvent")]
public class GyroEvent : ScriptableObject
{
    public Action<Gyroscope> OnGyroAttitude;
}