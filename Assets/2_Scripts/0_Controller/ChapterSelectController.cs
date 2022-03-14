using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterSelectController : MonoBehaviour
{
    public ChapterSwipeEvent swipeEvent;
    public ChapterSlideEvent slideEvent;
    public ChapterClickEvent clickEvent;

    public ChapterSwipeView chapterView;




    private void Start()
    {
        chapterView.InitializeButtons();
    }

    private void OnEnable()
    {
        swipeEvent.OnSwipe += OnSwipe;
        swipeEvent.OnSwipeEnd += OnSwipeEnd;
        slideEvent.OnSlide += OnSlide;
        clickEvent.OnClick += OnClick;
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

    private void OnSlide(int value)
    {
        chapterView.MoveToIndex(value);
    }

    private void OnClick(int index)
    {
        chapterView.MoveToIndex(index);
    }

}
