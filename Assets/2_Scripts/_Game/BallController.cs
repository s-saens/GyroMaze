using UnityEngine;

public class BallController : MonoBehaviour
{
    // Register
    [SerializeField] private EventVector2 completeEvent_;

    // Invoke
    [SerializeField] private Event sinkEndEvent;

    private Rigidbody2D rigid;

    private void OnEnable()
    {
        completeEvent_.callback += SinkBall;
    }

    private void OnDisable()
    {
        completeEvent_.callback -= SinkBall;
    }

    private void SinkBall(Vector2 endPoint)
    {
        rigid.AddForceTo(endPoint);
        this.transform.ScaleTo(Vector2.zero).Then(() => sinkEndEvent.Invoke());
    }
}