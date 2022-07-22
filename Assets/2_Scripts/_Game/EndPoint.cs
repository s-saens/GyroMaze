using UnityEngine;
using Newtonsoft.Json;

public class EndPoint : MonoBehaviour
{
    [SerializeField] private Event completeEvent1;

    private bool isTriggered = false;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        completeEvent1.Invoke();
    }
}