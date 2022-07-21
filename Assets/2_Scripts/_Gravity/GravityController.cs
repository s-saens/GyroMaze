using UnityEngine;

public class GravityController : MonoBehaviour
{
    [SerializeField] private EventVector2 gravityEvent;

    private void OnEnable()
    {
        gravityEvent.callback += ChangeGravity;
    }
    private void OnDisable()
    {
        gravityEvent.callback -= ChangeGravity;
    }

    private void ChangeGravity(Vector2 gravity)
    {
        Debug.Log(gravity);
        Physics2D.gravity = gravity;
    }
}
