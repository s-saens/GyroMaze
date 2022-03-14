using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ZoomEvent", menuName = "Events/ZoomEvent")]
public class ZoomEvent : ScriptableObject
{
    public Action<float> OnZoom;
}