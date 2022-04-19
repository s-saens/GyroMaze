using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

#if UNITY_EDITOR
[CustomEditor(typeof(_EventButton), true)]
public class EventButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        _EventButton t = (_EventButton)target;
    }
}
#endif

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

    public void SetClickEvent(ClickEvent ce)
    {
        clickEvent = ce;
        InitButton();
    }

}