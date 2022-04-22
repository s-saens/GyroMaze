using UnityEngine;

public class C_WindowChanger : MonoBehaviour
{
    // Events
    [SerializeField] private ClickEvent chapterButtonClickEvent;
    [SerializeField] private ClickEvent stageButtonClickEvent;
    [SerializeField] private ClickEvent backButtonClickEvent;


    [SerializeField] private ChapterSelectionView chapterSelectionView;
    [SerializeField] private StageSelectionView stageSelectionView;


    private void OnEnable()
    {
        chapterButtonClickEvent.OnClick += OnChpaterButton;
        stageButtonClickEvent.OnClick += OnStageButton;
        backButtonClickEvent.OnClick += OnBackButton;
    }

    private void OnDisable()
    {
        chapterButtonClickEvent.OnClick -= OnChpaterButton;
        stageButtonClickEvent.OnClick -= OnStageButton;
        backButtonClickEvent.OnClick -= OnBackButton;
    }

    private void OnChpaterButton(int value)
    {
        GlobalData.chapterIndex.value = value;
        chapterSelectionView.gameObject.SetActive(false);
        stageSelectionView.gameObject.SetActive(true);
        stageSelectionView.UpdateButtons();
    }

    private void OnStageButton(int value)
    {
        C_Scene.Instance.LoadScene(SceneEnum.Level);
    }

    private void OnBackButton(int value)
    {
        chapterSelectionView.gameObject.SetActive(true);
        chapterSelectionView.Magnet();
        stageSelectionView.gameObject.SetActive(false);
    }

}