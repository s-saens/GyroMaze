using UnityEngine;
using UnityEngine.EventSystems;

public class DragInput : MonoBehaviour
{
    public DragEvent dragEvent;


    private void Update()
    {
        if (Input.touchCount != 1) return;

        Touch t = Input.touches[0];
        Vector2 draggingVector = t.deltaPosition;

        dragEvent.OnDrag.Invoke(draggingVector);
    }
}
