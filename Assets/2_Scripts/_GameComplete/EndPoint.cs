using UnityEngine;
using Newtonsoft.Json;

public class EndPoint : MonoBehaviour
{
    [SerializeField] private EventGameComplete completeEvent;

    [SerializeField] private GameCompleteInfo info;

    void OnTriggerEnter(Collider coll)
    {
        if(coll.tag != "Ball") return;

        info.ballR = coll.GetComponent<Rigidbody2D>();
        info.ballT = coll.transform;
        info.endPointT = this.transform;
        
        completeEvent.Invoke(info);
    }
}