using UnityEngine;

public class C_WindowChanger : MonoBehaviour
{
    // Events
    [SerializeField] private ClickEvent chapterButtonClickEvent;
    [SerializeField] private ClickEvent stageButtonClickEvent;
    [SerializeField] private ClickEvent backButtonClickEvent;


    [SerializeField] private GameObject chapterSelectionView;
    [SerializeField] private GameObject stageSelectionView;

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
        chapterSelectionView.SetActive(false);
        stageSelectionView.SetActive(true);
    }

    private void OnStageButton(int value)
    {
        C_Scene.Instance.LoadScene(SceneEnum.Level);
    }

    private void OnBackButton(int value)
    {
        chapterSelectionView.SetActive(true);
        stageSelectionView.SetActive(false);
    }

}