using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] private Event sinkEndEvent_;
    [SerializeField] private Event loadNextEvent;


    [SerializeField] private float minimumOrthographicSize = 0.001f;
    [SerializeField] private float maximumOrthographicSize = 1000f;

    private Camera cam;
    
    private void OnEnable()
    {
        sinkEndEvent_.callback += ZoomIn;
    }
    private void OnDisable()
    {
        sinkEndEvent_.callback -= ZoomIn;
    }

    private void ZoomIn()
    {
        Zoom(minimumOrthographicSize);
    }
    private void ZoomOut()
    {
        Zoom(maximumOrthographicSize);
    }

    private void Zoom(float orthSize)
    {
        cam.OrthographicSizeTo(orthSize).Then(() => loadNextEvent.Invoke());
    }
}