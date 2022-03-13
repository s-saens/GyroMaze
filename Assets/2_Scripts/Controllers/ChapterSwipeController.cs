using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterSwipeController : MonoBehaviour
{
    public ChapterSwipeEvent swipeEvent;
    public ChapterSwipeView chapterView;

    private void OnEnable()
    {
        swipeEvent.OnSwipe += OnSwipe;
        swipeEvent.OnSwipeEnd += OnSwipeEnd;
    }

    private void OnDisable()
    {
        swipeEvent.OnSwipe -= OnSwipe;
        swipeEvent.OnSwipeEnd -= OnSwipeEnd;
    }

    private void OnSwipe(float deltaX)
    {
        chapterView.OnSwipe(deltaX);
    }

    private void OnSwipeEnd(float deltaX)
    {
        chapterView.OnSwipeEnd(deltaX);
    }
}
