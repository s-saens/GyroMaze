using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;


public class ChapterViewController : MonoBehaviour
{
    // Data
    public ChapterViewData viewData;
    public GlobalData globalData;

    // Events
    public SwipeEvent swipeEvent;
    public ChapterSlideEvent slideEvent;
    public ChapterClickEvent clickEvent;
    public EventSystem eventSystem;

    // Views
    public ChapterView chapterSwipeView;


    // Register Events
    private void OnEnable()
    {
        globalData.chapterIndex.onChange += OnChangeNowIndex;

        swipeEvent.OnTouchDown += OnTouchDown;
        swipeEvent.OnSwipe += OnSwipe;
        swipeEvent.OnSwipeEnd += OnSwipeEnd;
        slideEvent.OnSlide += OnSlide;
        clickEvent.OnClick += OnClick;
    }

    private void OnDisable()
    {
        globalData.chapterIndex.onChange -= OnChangeNowIndex;

        swipeEvent.OnTouchDown -= OnTouchDown;
        swipeEvent.OnSwipe -= OnSwipe;
        swipeEvent.OnSwipeEnd -= OnSwipeEnd;
        slideEvent.OnSlide -= OnSlide;
        clickEvent.OnClick -= OnClick;
    }


    // Events
    private void OnTouchDown()
    {
        chapterSwipeView.Magnet();
    }

    private void OnSwipe(float deltaX)
    {
        chapterSwipeView.Swipe(deltaX);
    }

    private void OnSwipeEnd(float deltaX)
    {
        chapterSwipeView.SwipeEnd(deltaX);
    }

    private void OnSlide(int value)
    {
        chapterSwipeView.MoveToIndex(value);
    }

    private void OnClick(int index)
    {
        eventSystem.SetSelectedGameObject(null);
        if(index == globalData.chapterIndex.value)
        {
            // TODO: Move to Level Select Window
        }
        else
        {
            chapterSwipeView.MoveToIndex(index);
        }
    }

    // Change Data
    private void OnChangeNowIndex(int index)
    {
        slideEvent.OnSlide -= OnSlide;
        chapterSwipeView.SetSliderHandle(index);
        slideEvent.OnSlide += OnSlide;
    }
}