using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ChapterViewData", menuName = "Data/ChapterViewData")]
public class ChapterViewData : ScriptableObject
{
    public int chapterCount = 5;
    public int visibleChapterCount = 3;
    public int maxDeltaX = 100;
    public int originalSize = 500;
    public int minSize = 400;
    public float stopTime = 200;
    public float magnetLerpTime = 0.5f;
}