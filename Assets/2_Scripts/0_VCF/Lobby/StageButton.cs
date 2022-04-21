using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum StageButtonState
{
    LOCKED,
    OPENED,
    CLOSED
}

public class StageButton : _EventButton
{
    // State Changing
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Sprite circleFull;
    [SerializeField] private Sprite circleEmpty;
    [SerializeField] private Sprite circleLock;

    public void SetState(StageButtonState value)
    {
        switch(value)
        {
            case StageButtonState.LOCKED : SetLocked(); break;
            case StageButtonState.OPENED : SetOpened(); break;
            case StageButtonState.CLOSED: SetClosed(); break;
        }
        levelText.text = (buttonId + 1).ToString();
    }
    private void SetLocked()
    {
        image.sprite = circleLock;

        levelText.gameObject.SetActive(false);
    }
    private void SetOpened()
    {
        image.sprite = circleEmpty;

        levelText.gameObject.SetActive(true);

        Color color;
        ColorUtility.TryParseHtmlString("#FFFFFF", out color);
        levelText.color = color;
    }
    private void SetClosed()
    {
        image.sprite = circleFull;

        levelText.gameObject.SetActive(true);

        Color color;
        ColorUtility.TryParseHtmlString("#5245DB", out color);
        levelText.color = color;
    }
}