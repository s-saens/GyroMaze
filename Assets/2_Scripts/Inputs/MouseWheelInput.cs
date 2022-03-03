using UnityEngine;
using UnityEngine.EventSystems;

public class MouseWheelInput : MonoBehaviour, IScrollHandler
{
    public ZoomEvent zoomEvent;
    public void OnScroll(PointerEventData e)
    {
        float scrollDelta = Mathf.Sign(e.scrollDelta.y);
        zoomEvent.OnZoom?.Invoke(scrollDelta);
    }
}