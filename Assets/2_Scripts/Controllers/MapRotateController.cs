using UnityEngine;

public class MapRotateController : MonoBehaviour
{
    public DragEvent dragEvent;
    public Transform horizontalPivot;
    public Transform verticalPivot;
    public Transform cam;

    public int rotationLimit = 90;
    public float rotationSpeed = 1;

    public bool lockV = false;
    public bool lockH = false;

    public void OnEnable()
    {
        dragEvent.OnDrag += OnDrag;
    }

    public void OnDisable()
    {
        dragEvent.OnDrag -= OnDrag;
    }

    float wh = Screen.width * Screen.height;
    public void OnDrag(Vector2 dragVector)
    {
        float vertical = -(dragVector.y) * 180 / Screen.width * rotationSpeed; // x축 회전 담당
        float horizontal = (dragVector.x) * 180 / Screen.height * rotationSpeed; // z축 회전 담당

        float rotatedRotationV = verticalPivot.transform.rotation.eulerAngles.x + vertical;
        float rotatedRotationH = horizontalPivot.transform.rotation.eulerAngles.z + horizontal;

        if(rotatedRotationV > 360-rotationLimit || rotatedRotationV < rotationLimit && lockV == false)
            verticalPivot.transform.Rotate(vertical, 0, 0, Space.Self);
        if(rotatedRotationH > 360 - rotationLimit || rotatedRotationH < rotationLimit && lockH == false)
            horizontalPivot.transform.Rotate(0, 0, horizontal, Space.Self);
    }

    private void Update()
    {
        UpdateGravity();
    }

    private void UpdateGravity()
    {
        Physics.gravity = cam.transform.forward * 12;
    }
}