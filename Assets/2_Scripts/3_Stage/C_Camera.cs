using UnityEngine;

public class C_Camera : MonoBehaviour
{
    public Event zoomEvent;

    public Transform cam;
    public Transform target;

    public float minCamPositionY = 3;
    public float camPositionY = 10;
    public float maxCamPositionY = 70;
    public float lerpTime = 0.2f;

    public float zoomStrength = 2;




    public void OnEnable()
    {
        zoomEvent.callback += OnZoom;
    }

    public void OnDisable()
    {
        zoomEvent.callback -= OnZoom;
    }


    private void Update()
    {
        Vector3 lastPos = cam.position;
        Vector3 targetPos = target.position;
        targetPos.y = camPositionY;
        cam.position = Vector3.Lerp(lastPos, targetPos, lerpTime);
    }

    private void OnZoom(string param) // float, amount < 0 : 확대
    {
        float amount = float.Parse(param);
        camPositionY -= amount * zoomStrength;

        if(camPositionY < minCamPositionY) // 더 확대할 수 없는 경우인데 확대하려는 경우
        {
            camPositionY = minCamPositionY;
        }
        if(camPositionY > maxCamPositionY) // 더 축소할 수 없는 경우인데 축소하려는 경우
        {
            camPositionY = maxCamPositionY;
        }
    }
}
