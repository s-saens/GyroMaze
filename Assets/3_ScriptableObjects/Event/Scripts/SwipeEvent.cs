using UnityEngine;
using System;

[CreateAssetMenu(fileName = "StageSwipeEvent", menuName = "Events/StageSwipeEvent")]
public class SwipeEvent : ScriptableObject
{
    public Action OnTouchDown;
    public Action<float> OnSwipe;
    public Action<float> OnSwipeEnd;

}