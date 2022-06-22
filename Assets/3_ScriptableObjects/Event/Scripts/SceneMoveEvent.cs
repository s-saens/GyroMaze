using UnityEngine;
using System;

[CreateAssetMenu(fileName = "SceneMoveEvent", menuName = "Events/SceneMoveEvent")]
public class SceneMoveEvent : ScriptableObject
{
    public Action<Vector2> OnDrag;
}