using UnityEngine;

public class GameCompleteController : MonoBehaviour
{
    [SerializeField] private string completeViewIndex = "3";
    [SerializeField] private Event endPointCollisionEvent;
    [SerializeField] private Event viewToggleEvent;

    private void OnEnable()
    {
        endPointCollisionEvent.callback += GameComplete;
    }
    private void OnDisable()
    {
        endPointCollisionEvent.callback -= GameComplete;
    }


    private void GameComplete(object f)
    {
        viewToggleEvent.Invoke((string)completeViewIndex);
    }
}