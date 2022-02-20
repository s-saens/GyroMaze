using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapRotateInput : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    public DragEvent dragEvent;

    Vector3 lastPos;
    Vector3 draggingPos;

    public void OnBeginDrag(PointerEventData beginPoint)
    {
        lastPos = beginPoint.position;
    }
        public void OnDrag(PointerEventData draggingPoint)
    {
        draggingPos = draggingPoint.position;

        Vector2 dragVector = draggingPos - lastPos;

        lastPos = draggingPos;

        dragEvent.OnDrag?.Invoke(dragVector);
    }
}
