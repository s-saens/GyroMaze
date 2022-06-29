using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;


public class SwipeInput : MonoBehaviour, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Event swipeStartEvent;
    public Event swipingEvent;
    public Event swipeEndEvent;

    private float firstX;

    public void OnInitializePotentialDrag(PointerEventData e)
    {
        swipeStartEvent.callback?.Invoke("");
    }

    public void OnBeginDrag(PointerEventData e)
    {
        firstX = e.position.x;
    }

    public void OnDrag(PointerEventData e)
    {
        swipingEvent.callback?.Invoke((e.delta.x * Time.deltaTime * 60).ToString());
    }

    public void OnEndDrag(PointerEventData e)
    {
        swipeEndEvent.callback?.Invoke((e.delta.x * Time.deltaTime * 60).ToString());
    }

}
