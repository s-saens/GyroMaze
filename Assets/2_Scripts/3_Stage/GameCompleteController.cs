using UnityEngine;

public class GameCompleteController : MonoBehaviour
{
    [SerializeField] private string completeViewIndex = "3";
    [SerializeField] private CollideEvent endPointCollisionEvent;
    [SerializeField] private ButtonEvent viewToggleEvent;

    private void OnEnable()
    {
        endPointCollisionEvent.OnCollide += GameComplete;
    }
    private void OnDisable()
    {
        endPointCollisionEvent.OnCollide -= GameComplete;
    }


    private void GameComplete(float f)
    {
        viewToggleEvent.OnClick?.Invoke(completeViewIndex);
    }
}