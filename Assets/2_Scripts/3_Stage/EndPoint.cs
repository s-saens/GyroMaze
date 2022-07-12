using UnityEngine;

public class EndPoint : MonoBehaviour
{
    [SerializeField] private Event endPointCollisionEvent;

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.transform.tag == "Player")
        {
            endPointCollisionEvent.callback?.Invoke("");
        }
    }
}