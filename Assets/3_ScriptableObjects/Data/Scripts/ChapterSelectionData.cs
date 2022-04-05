using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ChapterSelectionData", menuName = "Data/ChapterSelectionData")]
public class ChapterSelectionData : ScriptableObject
{
    public int chapterCount = 50;
    public int visibleChapterCount = 3;
    public int maxDeltaX = 100;
    public int originalSize = 500;
    public int minSize = 400;
    public float stopTime = 300;
    public float magnetLerpTime = 10;
}