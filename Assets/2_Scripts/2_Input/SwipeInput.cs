using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;


public class SwipeInput : MonoBehaviour, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public SwipeEvent swipeEvent;

    private float firstX;

    public void OnInitializePotentialDrag(PointerEventData e)
    {
        swipeEvent.OnTouchDown?.Invoke();
    }

    public void OnBeginDrag(PointerEventData e)
    {
        firstX = e.position.x;
    }

    public void OnDrag(PointerEventData e)
    {
        swipeEvent.OnSwipe?.Invoke(e.delta.x);
    }

    public void OnEndDrag(PointerEventData e)
    {
        swipeEvent.OnSwipeEnd?.Invoke(e.delta.x);
    }

}
