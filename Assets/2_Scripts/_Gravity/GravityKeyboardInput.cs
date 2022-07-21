using UnityEngine;

public class GravityKeyboardInput : MonoBehaviour
{
    [SerializeField] private EventVector2 gravityEvent;

    private float gravityX = 0;
    private float gravityY = 0;

    void FixedUpdate()
    {
        gravityX = 0;
        gravityY = 0;
        if(Input.GetKey(KeyCode.A)) gravityX -= 10f;
        if(Input.GetKey(KeyCode.S)) gravityY -= 10f;
        if(Input.GetKey(KeyCode.D)) gravityX += 10f;
        if(Input.GetKey(KeyCode.W)) gravityY += 10f;

        gravityEvent.Invoke(new Vector2(gravityX, gravityY));
    }
}