using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ButtonEvent", menuName = "Events/ButtonEvent")]
public class ButtonEvent : ScriptableObject
{
    public Action<string> OnClick;
}