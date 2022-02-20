using UnityEngine;

public class MapRotateController : MonoBehaviour
{
    public DragEvent dragEvent;
    public Transform map;

    public float rotationSpeed;

    public void OnEnable()
    {
        dragEvent.OnDrag += OnDrag;
    }

    public void OnDisable()
    {
        dragEvent.OnDrag -= OnDrag;
    }

    public void OnDrag(Vector2 dragVector)
    {
        float vertical = (dragVector.y) * 180 / Screen.width * rotationSpeed; // x축 회전 담당
        float horizontal = (dragVector.x) * 90 / Screen.height * rotationSpeed; // z축 회전 담당

        map.transform.Rotate(vertical, 0, horizontal);
    }
}