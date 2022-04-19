using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Camera : MonoBehaviour
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

    private void OnZoom(float amount) // amount < 0 : 확대
    {
        float targetPositionY = this.cam.position.y + amount * zoomStrength;

        if(targetPositionY < minCamPositionY) // 더 확대할 수 없는 경우인데 확대하려는 경우
        {
            targetPositionY = minCamPositionY;
        }
        if(targetPositionY > maxCamPositionY) // 더 축소할 수 없는 경우인데 축소하려는 경우
        {
            targetPositionY = maxCamPositionY;
        }
        cam.localPosition = Vector3.up * targetPositionY;
    }
}
