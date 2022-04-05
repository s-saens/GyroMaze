using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ClickEvent", menuName = "Events/ClickEvent")]
public class ClickEvent : ScriptableObject
{
    public Action<int> OnClick;
}