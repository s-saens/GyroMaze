using UnityEngine;
using System;

[CreateAssetMenu(fileName = "CollideEvent", menuName = "Events/CollideEvent")]
public class CollideEvent : ScriptableObject
{
    public Action<float> OnCollide;
}