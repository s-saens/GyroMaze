using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ChapterClickEvent", menuName = "Events/ChapterClickEvent")]
public class ChapterClickEvent : ScriptableObject
{
    public Action<int> OnClick;
}