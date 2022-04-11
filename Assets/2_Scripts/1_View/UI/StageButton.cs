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

public class StageButton : MonoBehaviour
{
    public TMP_Text levelText;
    public RawImage image;
    public Texture circleFull;
    public Texture circleEmpty;
    public Texture circleLock;

    public Data<int> state = new Data<int>(0);

    private void OnEnable()
    {
        state.onChange += OnChangeState;
    }

    private void OnDisable()
    {
        state.onChange -= OnChangeState;
    }

    private void OnChangeState(int value)
    {
        switch((StageButtonState)value)
        {
            case StageButtonState.LOCKED : SetLocked(); break;
            case StageButtonState.OPENED : SetOpened(); break;
            case StageButtonState.CLOSED: SetClosed(); break;
        }
        levelText.text = value.ToString();
    }

    private void SetLocked()
    {
        image.texture = circleLock;

        levelText.gameObject.SetActive(false);
    }
    private void SetOpened()
    {
        image.texture = circleEmpty;

        levelText.gameObject.SetActive(true);

        Color color;
        ColorUtility.TryParseHtmlString("#FFFFFF", out color);
        levelText.color = color;
    }
    private void SetClosed()
    {
        image.texture = circleFull;

        levelText.gameObject.SetActive(true);

        Color color;
        ColorUtility.TryParseHtmlString("#5245DB", out color);
        levelText.color = color;
    }
}