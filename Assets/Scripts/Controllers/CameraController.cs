using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public ZoomEvent zoomEvent;

    public Transform cam;
    public Transform ball;

    public float camPositionY = 4;
    public float lerpTime = 0.2f;

    public float zoomStrength = 2;


    public void OnEnable()
    {
        zoomEvent.OnZoom += OnZoom;
    }

    public void OnDisable()
    {
        zoomEvent.OnZoom -= OnZoom;
    }


    private void Update()
    {
        Vector3 lastPos = cam.position;
        Vector3 targetPos = new Vector3(ball.position.x, ball.position.y + camPositionY, ball.position.z);
        cam.position = Vector3.Lerp(lastPos, targetPos, lerpTime);
    }

    private void OnZoom(float amount)
    {
        this.camPositionY -= amount * zoomStrength;
    }
}
