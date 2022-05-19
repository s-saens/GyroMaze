using UnityEngine;


public class C_StageSelection : MonoBehaviour
{
    // Data
    [SerializeField] private StageSelectionData viewData;

    // Events : Operation
    [SerializeField] private SwipeEvent swipeEvent;
    [SerializeField] private SlideEvent slideEvent;
    [SerializeField] private ClickEvent clickEvent;

    // Views
    [SerializeField] private StageSelectionView stageSwipeView;

    // Register Events
    private void OnEnable()
    {
        GameData.stageIndex.onChange += OnChangeStageIndex;

        swipeEvent.OnTouchDown += OnTouchDown;
        swipeEvent.OnSwipe += OnSwipe;
        swipeEvent.OnSwipeEnd += OnSwipeEnd;
        slideEvent.OnSlide += OnSlide;
        clickEvent.OnClick += OnClickStage;
    }

    private void OnDisable()
    {
        GameData.stageIndex.onChange -= OnChangeStageIndex;

        swipeEvent.OnTouchDown -= OnTouchDown;
        swipeEvent.OnSwipe -= OnSwipe;
        swipeEvent.OnSwipeEnd -= OnSwipeEnd;
        slideEvent.OnSlide -= OnSlide;
        clickEvent.OnClick -= OnClickStage;
    }


    // Events
    private void OnTouchDown()
    {
        stageSwipeView.Magnet();
    }

    private void OnSwipe(float deltaX)
    {
        stageSwipeView.Swipe(deltaX);
    }

    private void OnSwipeEnd(float deltaX)
    {
        stageSwipeView.SwipeEnd(deltaX);
    }

    private void OnSlide(int value)
    {
        stageSwipeView.MoveToIndex(value);
    }
    private void OnClickStage(int id)
    {
        C_Scene.Instance.LoadScene(SceneEnum.Level);
    }

    // Change Data
    private void OnChangeStageIndex(int index)
    {
        slideEvent.OnSlide -= OnSlide;
        stageSwipeView.SetSliderHandle(index);
        slideEvent.OnSlide += OnSlide;
    }
}