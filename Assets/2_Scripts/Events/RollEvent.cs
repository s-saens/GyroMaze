using UnityEngine;
using System;

[CreateAssetMenu(fileName = "RollEvent", menuName = "Events/RollEvent")]
public class RollEvent : ScriptableObject
{
    public Action<float> OnRoll;
}