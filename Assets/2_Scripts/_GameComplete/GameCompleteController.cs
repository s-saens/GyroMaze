using UnityEngine;

public class GameCompleteInfo
{
    public Transform ballT;
    public Rigidbody2D ballR;
    public Transform endPointT;
}

public class GameCompleteController : MonoBehaviour
{
#region ASSIGN EVENTS
    [SerializeField] private EventGameComplete completeEvent;
    [SerializeField] private Camera cam;

    private void OnEnable()
    {
        completeEvent.callback += CompleteGame;
    }
    private void OnDisable()
    {
        completeEvent.callback += CompleteGame;
    }
#endregion


    private void CompleteGame(GameCompleteInfo info)
    {
        StartCoroutine(info.ballR.AddForceTo(info.endPointT).Concat(cam.OrthographicSizeTo(0)));
        StartCoroutine(info.ballT.ScaleTo(Vector2.zero));
    }
}