using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragInput : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    public DragEvent dragEvent;
    public ZoomEvent zoomEvent;

    Vector3 lastPos;
    Vector3 draggingPos;

    private bool isTwoTouches = false;

    private void Update()
    {
        isTwoTouches = (Input.touchCount == 2);

        if(isTwoTouches)
        {
            Touch t1 = Input.GetTouch(0);
            Touch t2 = Input.GetTouch(1);

            Vector3 t1Tot2 = t2.position - t1.position;
            float dotProductDelta = Vector3.Dot(t1.deltaPosition, t2.deltaPosition);
            bool isPinch = dotProductDelta < 0;

            if(isPinch)
            {
                if(Vector3.Dot(t1Tot2, t1.deltaPosition) > 0) // 축소
                {
                    zoomEvent.OnZoom?.Invoke(dotProductDelta/1920); // 음수값 넘겨중
                }
                else // 확대
                {
                    zoomEvent.OnZoom?.Invoke(-dotProductDelta/1920);
                }
            }

        }
        
    }

    public void OnBeginDrag(PointerEventData beginPoint)
    {
        lastPos = beginPoint.position;
    }
    public void OnDrag(PointerEventData draggingPoint)
    {
        if(isTwoTouches) return;

        draggingPos = draggingPoint.position;

        Vector2 dragVector = draggingPos - lastPos;

        lastPos = draggingPos;

        dragEvent.OnDrag?.Invoke(dragVector);
    }
}
