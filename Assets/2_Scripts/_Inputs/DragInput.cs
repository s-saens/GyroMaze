using UnityEngine;
using Newtonsoft.Json;

public class DragInput : MonoBehaviour
{
    public EventVector3 dragEvent;


    private void Update()
    {
        if (Input.touchCount != 1) return;

        Touch t = Input.touches[0];
        Vector2 draggingVector = t.deltaPosition;

        dragEvent.Invoke(draggingVector * Time.deltaTime * 60);
    }
}
