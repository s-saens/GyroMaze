using UnityEngine;
using Newtonsoft.Json;

public class DragInput : MonoBehaviour
{
    public Event dragEvent;


    private void Update()
    {
        if (Input.touchCount != 1) return;

        Touch t = Input.touches[0];
        Vector2 draggingVector = t.deltaPosition;

        dragEvent.Invoke(JsonConvert.SerializeObject(draggingVector * Time.deltaTime * 60, JsonSettings.Settings));
    }
}
