using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;


public class C_ChapterSelection : MonoBehaviour
{
    // Data
    [SerializeField] private ChapterSelectionData viewData;
    [SerializeField] private GlobalData globalData;

    // Events : Operation
    [SerializeField] private SwipeEvent swipeEvent;
    [SerializeField] private SlideEvent slideEvent;
    [SerializeField] private ClickEvent clickEvent;
    [SerializeField] private EventSystem eventSystem;

    // Views
    [SerializeField] private ChapterSelectionView chapterSwipeView;

    // Register Events
    private void OnEnable()
    {
        globalData.chapterIndex.onChange += OnChangeChapterIndex;

        swipeEvent.OnTouchDown += OnTouchDown;
        swipeEvent.OnSwipe += OnSwipe;
        swipeEvent.OnSwipeEnd += OnSwipeEnd;
        slideEvent.OnSlide += OnSlide;
    }

    private void OnDisable()
    {
        globalData.chapterIndex.onChange -= OnChangeChapterIndex;

        swipeEvent.OnTouchDown -= OnTouchDown;
        swipeEvent.OnSwipe -= OnSwipe;
        swipeEvent.OnSwipeEnd -= OnSwipeEnd;
        slideEvent.OnSlide -= OnSlide;
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

    // Change Data
    private void OnChangeChapterIndex(int index)
    {
        slideEvent.OnSlide -= OnSlide;
        chapterSwipeView.SetSliderHandle(index);
        slideEvent.OnSlide += OnSlide;
    }
}