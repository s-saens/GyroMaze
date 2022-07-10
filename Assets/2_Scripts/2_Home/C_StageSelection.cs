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
    [SerializeField] private Event slideEvent;
    [SerializeField] private Event buttonEvent;

    // Views
    [SerializeField] private StageSelectionView stageSwipeView;

    // Register Events
    private void OnEnable()
    {
        GameData.stageIndex.onChange += OnChangeStageIndex;

        swipeStartEvent.callback += OnTouchDown;
        swipingEvent.callback += OnSwipe;
        swipeEndEvent.callback += OnSwipeEnd;
        slideEvent.callback += OnSlide;
        buttonEvent.callback += OnClickStage;
    }

    private void OnDisable()
    {
        GameData.stageIndex.onChange -= OnChangeStageIndex;

        swipeStartEvent.callback -= OnTouchDown;
        swipingEvent.callback -= OnSwipe;
        swipeEndEvent.callback -= OnSwipeEnd;
        slideEvent.callback -= OnSlide;
        buttonEvent.callback -= OnClickStage;
    }


    // Events
    private void OnTouchDown(string s)
    {
        stageSwipeView.Magnet();
    }

    private void OnSwipe(string deltaX) // float
    {
        stageSwipeView.Swipe(float.Parse(deltaX));
    }

    private void OnSwipeEnd(string deltaX) // float
    {
        stageSwipeView.SwipeEnd(float.Parse(deltaX));
    }

    private void OnSlide(string value)
    {
        stageSwipeView.MoveToIndex(int.Parse(value));
    }
    private void OnClickStage(string id)
    {
        GameData.stageIndex.value = int.Parse(id);
        SceneController.Instance.LoadScene(SceneEnum.Stage);
    }

    // Change Data
    private void OnChangeStageIndex(int index)
    {
        slideEvent.callback -= OnSlide;
        stageSwipeView.SetSliderHandle(index);
        slideEvent.callback += OnSlide;
    }
}