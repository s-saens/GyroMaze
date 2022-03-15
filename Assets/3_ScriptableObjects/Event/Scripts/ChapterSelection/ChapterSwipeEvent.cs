using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ChapterSwipeEvent", menuName = "Events/ChapterSwipeEvent")]
public class ChapterSwipeEvent : ScriptableObject
{
    public Action OnTouchDown;
    public Action<float> OnSwipe;
    public Action<float> OnSwipeEnd;

}