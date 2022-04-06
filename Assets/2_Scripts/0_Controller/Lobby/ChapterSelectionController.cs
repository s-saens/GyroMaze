using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;


public class ChapterSelectionController : MonoBehaviour
{
    // Data
    public ChapterSelectionData viewData;
    public GlobalData globalData;

    // Events
    public SwipeEvent swipeEvent;
    public SlideEvent slideEvent;
    public ClickEvent clickEvent;
    public EventSystem eventSystem;

    // Views
    public ChapterSelectionView chapterSwipeView;


    // Register Events
    private void OnEnable()
    {
        globalData.chapterIndex.onChange += OnChangeChapterIndex;

        swipeEvent.OnTouchDown += OnTouchDown;
        swipeEvent.OnSwipe += OnSwipe;
        swipeEvent.OnSwipeEnd += OnSwipeEnd;
        slideEvent.OnSlide += OnSlide;
        clickEvent.OnClick += OnClick;
    }

    private void OnDisable()
    {
        globalData.chapterIndex.onChange -= OnChangeChapterIndex;

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
    private void OnChangeChapterIndex(int index)
    {
        slideEvent.OnSlide -= OnSlide;
        chapterSwipeView.SetSliderHandle(index);
        slideEvent.OnSlide += OnSlide;
    }
}