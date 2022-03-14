using UnityEngine;
using System;

[CreateAssetMenu(fileName = "DragEvent", menuName = "Events/DragEvent")]
public class DragEvent : ScriptableObject
{
    public Action<Vector2> OnDrag;
}