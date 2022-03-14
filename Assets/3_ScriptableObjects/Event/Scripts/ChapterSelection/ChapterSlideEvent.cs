using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ChapterSlideEvent", menuName = "Events/ChapterSlideEvent")]
public class ChapterSlideEvent : ScriptableObject
{
    public Action<int> OnSlide;

}