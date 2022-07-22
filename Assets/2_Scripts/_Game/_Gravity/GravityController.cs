using UnityEngine;

public class GravityController : MonoBehaviour
{
    [SerializeField] private int strength = 20;
    [SerializeField] private EventVector2 gravityEvent_;

    private void OnEnable()
    {
        gravityEvent_.callback += ChangeGravity;
    }
    private void OnDisable()
    {
        gravityEvent_.callback -= ChangeGravity;
    }

    private void ChangeGravity(Vector2 gravity)
    {
        Physics2D.gravity = gravity * strength;
    }
}
