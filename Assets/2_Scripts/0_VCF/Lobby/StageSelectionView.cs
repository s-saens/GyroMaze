using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class StageSelectionView : MonoBehaviour
{
    // Data
    [SerializeField] private UserData userData;
    [SerializeField] private GlobalData globalData;

    // Factory
    [SerializeField] private StageButtonFactory factory;

    // Views
    [SerializeField] private List<StageButton> buttons;

    private void Start()
    {
        buttons = factory.MakeButtons(20);
        OnEnable();
    }

    private void OnEnable()
    {
        UpdateButtons();
    }

    public void UpdateButtons()
    {
        for(int i=0 ; i<buttons.Count ; ++i)
        {
            StageButton b = buttons[i];

            if (globalData.chapterIndex.value > userData.chapter.value - 1) b.SetState(StageButtonState.LOCKED);
            else if (globalData.chapterIndex.value < userData.chapter.value - 1) b.SetState(StageButtonState.CLOSED);
            else
            {
                if (i == userData.stage.value - 1) b.SetState(StageButtonState.OPENED);
                else if (i < userData.stage.value - 1) b.SetState(StageButtonState.CLOSED);
                else b.SetState(StageButtonState.LOCKED);
            }
        }
    }
}