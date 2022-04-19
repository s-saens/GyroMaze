using UnityEngine;
using UnityEngine.UI;

public class _EventButton : Button
{
    [SerializeField] public int buttonId = 0;
    [SerializeField] private ClickEvent clickEvent;

    protected override void Start()
    {
        base.Start();
        InitButton();
    }

    private void InitButton()
    {
        onClick.AddListener(()=>clickEvent.OnClick?.Invoke(buttonId));
    }

}