using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ChapterSwipeEvent", menuName = "Events/ChapterSwipeEvent")]
public class SwipeEvent : ScriptableObject
{
    public Action OnTouchDown;
    public Action<float> OnSwipe;
    public Action<float> OnSwipeEnd;

}