using UnityEngine;

public class MapRotateController : MonoBehaviour
{
    public DragEvent dragEvent;
    public Transform horizontalPivot;
    public Transform verticalPivot;

    public int rotationLimit = 90;
    public float rotationSpeed = 1;

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
        float horizontal = -(dragVector.x) * 90 / Screen.height * rotationSpeed; // z축 회전 담당

        float rotatedRotationV = verticalPivot.transform.rotation.eulerAngles.x + vertical;
        float rotatedRotationH = horizontalPivot.transform.rotation.eulerAngles.z + horizontal;

        if(rotatedRotationV > 360-rotationLimit || rotatedRotationV < rotationLimit)
            verticalPivot.transform.Rotate(vertical, 0, 0, Space.Self);
        if(rotatedRotationH > 360 - rotationLimit || rotatedRotationH < rotationLimit)
            horizontalPivot.transform.Rotate(0, 0, horizontal, Space.Self);
        
    }
}