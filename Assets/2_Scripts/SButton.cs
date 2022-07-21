using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Animator animator;

    [SerializeField] public string parameter;
    [SerializeField] private EventString[] buttonEvents;

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

    private bool isDown = false;
    private bool isEnter = false;
    public void OnPointerDown(PointerEventData e)
    {
        if(!isDown)
        {
            animator?.SetTrigger(down);
            isDown = true;
        }
    }
    public void OnPointerUp(PointerEventData e)
    {
        if(isEnter)
        {
            animator?.SetTrigger(enter);
        }
        else
        {
            animator.SetTrigger(normal);
        }
        isDown = false;
    }
    public void OnPointerEnter(PointerEventData e)
    {
        isEnter = true;
        if(isDown) animator.SetTrigger(down);
        else animator.SetTrigger(enter);
    }
    public void OnPointerExit(PointerEventData e)
    {
        isEnter = false;
        if(!isDown) animator.SetTrigger(normal);
    }

    public void OnPointerClick(PointerEventData e)
    {
        foreach(EventString b in buttonEvents)
        {
            b.Invoke(parameter);
        }
    }
}
