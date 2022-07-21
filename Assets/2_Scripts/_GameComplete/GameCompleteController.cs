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
        Debug.Log(info.ballR.velocity.magnitude);
        StartCoroutine(info.ballR.AddForceTo(info.endPointT.position).Then(()=>Debug.Log("!")));
        StartCoroutine(info.ballT.ScaleTo(Vector2.zero));
    }
}