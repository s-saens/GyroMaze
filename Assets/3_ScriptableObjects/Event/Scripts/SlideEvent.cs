using UnityEngine;
using System;

[CreateAssetMenu(fileName = "SlideEvent", menuName = "Events/SlideEvent")]
public class SlideEvent : ScriptableObject
{
    public Action<int> OnSlide;
}