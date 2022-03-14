using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ChapterSelectData", menuName = "Data/ChapterSelectData")]
public class ChapterSelectData : ScriptableObject
{

    public int chapterCount = 5;
    public int visibleChapterCount = 3;
    public int nowIndex = 0;
    public int maxDeltaX = 100;
    public int originalSize = 500;
    public int smallSize = 400;
    public float stopTime = 0.6f;
    public float magnetLerpTime = 0.5f;
}