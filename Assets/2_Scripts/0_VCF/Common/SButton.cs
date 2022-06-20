using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Animator animator;

    [SerializeField] public string parameter;
    [SerializeField] private ButtonEvent buttonEvent;

    private readonly int normal = Animator.StringToHash("Normal");
    private readonly int enter = Animator.StringToHash("Highlighted");
    private readonly int down = Animator.StringToHash("Pressed");


    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        animator = this.GetComponent<Animator>();
    }

    private bool isDown;
    private bool isEnter;
    public void OnPointerDown(PointerEventData e)
    {
        animator.SetTrigger(down);
        isDown = true;
    }
    public void OnPointerUp(PointerEventData e)
    {
        if(isEnter)
        {
            animator.SetTrigger(enter);
            buttonEvent.OnClick?.Invoke(parameter);
        }

        else animator.SetTrigger(normal);
        isDown = false;
    }
    public void OnPointerEnter(PointerEventData e)
    {
        if(isDown) animator.SetTrigger(enter);
        else animator.SetTrigger(down);
        isEnter = true;
    }
    public void OnPointerExit(PointerEventData e)
    {
        animator.SetTrigger(normal);
        isEnter = false;
    }
}
