using UnityEngine;

public class GravityKeyboardInput : MonoBehaviour
{
    private Rigidbody2D ball;
    void Start()
    {
        ball = this.GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.A)) ball.AddForce(Vector2.left);
        if(Input.GetKey(KeyCode.S)) ball.AddForce(Vector2.down);
        if(Input.GetKey(KeyCode.D)) ball.AddForce(Vector2.right);
        if(Input.GetKey(KeyCode.W)) ball.AddForce(Vector2.up);
    }
}