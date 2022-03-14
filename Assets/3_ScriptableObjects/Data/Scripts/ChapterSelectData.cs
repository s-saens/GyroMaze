using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ChapterSelectData", menuName = "Data/ChapterSelectData")]
public class ChapterSelectData : ScriptableObject
{
    public int chapterCount = 5;
    public int visibleChapterCount = 3;
    public int maxDeltaX = 100;
    public int originalSize = 500;
    public int minSize = 400;
    public float stopTime = 200;
    public float magnetLerpTime = 0.5f;


    // Reactive
    public Action<int> OnChangeNowIndex;
    private int nowIndex = 0;
    public int NowIndex
    {
        get
        {
            return nowIndex;
        }
        set
        {
            nowIndex = value;
            OnChangeNowIndex?.Invoke(nowIndex);
        }
    }

}