using UnityEngine;
using Newtonsoft.Json;

public class EndPoint : MonoBehaviour
{
    [SerializeField] private EventGameComplete completeEvent;

    [SerializeField] private GameCompleteInfo info = new GameCompleteInfo();

    private bool isTriggered = false;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag != "Ball" || isTriggered) return;

        info.ballR = coll.GetComponent<Rigidbody2D>();
        info.ballT = coll.transform;
        info.endPointT = this.transform;
        isTriggered = true;

        completeEvent.Invoke(info);
    }
}