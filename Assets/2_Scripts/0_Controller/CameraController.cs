using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public ZoomEvent zoomEvent;

    public Transform camPivot;
    public Transform cam;
    public Transform target;

    public float minCamPositionY = 3;
    public float maxCamPositionY = 70;
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
        Vector3 lastPos = camPivot.position;
        Vector3 targetPos = target.position;
        camPivot.position = Vector3.Lerp(lastPos, targetPos, lerpTime);
    }

    private void OnZoom(float amount)
    {
        float targetPositionY = this.cam.position.y - amount * zoomStrength;
        cam.localPosition = Vector3.up * Mathf.Clamp(targetPositionY, minCamPositionY, maxCamPositionY);
    }
}
