using UnityEngine;
using System;

public class C_StageSelection : MonoBehaviour
{
    // Data
    [SerializeField] private StageSelectionData viewData;

    // Events : Operation
    [SerializeField] private Event swipeStartEvent;
    [SerializeField] private Event swipingEvent;
    [SerializeField] private Event swipeEndEvent;
    [SerializeField] private Event sliderEvent;
    [SerializeField] private Event stageSelectionEvent;

    // Views
    [SerializeField] private StageSelectionView stageSwipeView;

    // Register Events
    private void OnEnable()
    {
        GameData.stageIndex.onChange += OnChangeStageIndex;

        swipeStartEvent.callback += OnTouchDown;
        swipingEvent.callback += OnSwipe;
        swipeEndEvent.callback += OnSwipeEnd;
        sliderEvent.callback += OnSlider;
        stageSelectionEvent.callback += OnClickStage;
    }

    private void OnDisable()
    {
        GameData.stageIndex.onChange -= OnChangeStageIndex;

        swipeStartEvent.callback -= OnTouchDown;
        swipingEvent.callback -= OnSwipe;
        swipeEndEvent.callback -= OnSwipeEnd;
        sliderEvent.callback -= OnSlider;
        stageSelectionEvent.callback -= OnClickStage;
    }


    // Events
    private void OnTouchDown(object param)
    {
        stageSwipeView.Magnet();
    }

    private void OnSwipe(object deltaX) // float
    {
        stageSwipeView.Swipe((float)deltaX);
    }

    private void OnSwipeEnd(object deltaX) // float
    {
        stageSwipeView.SwipeEnd((float)deltaX);
    }

    private void OnSlider(object value)
    {
        stageSwipeView.MoveToIndex((int)value);
    }
    private void OnClickStage(object id)
    {
        GameData.stageIndex.value = (int)id;
    }

    // Change Data
    private void OnChangeStageIndex(int index)
    {
        sliderEvent.callback -= OnSlider;
        stageSwipeView.SetSliderHandle(index);
        sliderEvent.callback += OnSlider;
    }
}