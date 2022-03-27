using UnityEngine;
using UnityEngine.UI;

public class LevelSelectController : MonoBehaviour
{
    // Data
    public GlobalData globalData;

    // Views
    public Transform parentTransform;
    private Button[] buttons;

    private void Start()
    {
        SetButtons();
        SetButtonsListeners();
    }

    private void SetButtons()
    {
        buttons = parentTransform.GetComponentsInChildren<Button>();
    }

    private void SetButtonsListeners()
    {
        foreach(Button b in buttons)
        {
        }
    }
}