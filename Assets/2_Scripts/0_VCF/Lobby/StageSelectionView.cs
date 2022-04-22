using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class StageSelectionView : MonoBehaviour
{
    // Data

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

            if (GlobalData.chapterIndex.value > UserData.chapter.value - 1) b.SetState(StageButtonState.LOCKED);
            else if (GlobalData.chapterIndex.value < UserData.chapter.value - 1) b.SetState(StageButtonState.CLOSED);
            else
            {
                if (i == UserData.stage.value - 1) b.SetState(StageButtonState.OPENED);
                else if (i < UserData.stage.value - 1) b.SetState(StageButtonState.CLOSED);
                else b.SetState(StageButtonState.LOCKED);
            }
        }
    }
}