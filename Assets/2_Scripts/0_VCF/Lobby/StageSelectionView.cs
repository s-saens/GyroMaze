using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class StageSelectionView : MonoBehaviour
{
    // Data
    public GlobalData globalData;

    // Factory
    public StageButtonFactory factory;

    // Views
    public List<StageButton> buttons;

    private void Start()
    {
        buttons = factory.MakeButtons(20);
    }
}