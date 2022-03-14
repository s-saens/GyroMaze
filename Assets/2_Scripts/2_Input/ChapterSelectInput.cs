using UnityEngine;
using UnityEngine.EventSystems;

public class ChapterSwipeInput : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ChapterSwipeEvent swipeEvent;

    private float firstX;

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