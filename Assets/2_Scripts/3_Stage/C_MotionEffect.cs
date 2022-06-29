using UnityEngine;

public class C_MotionEffect : MonoBehaviour
{
    public AudioSource rollSound;
    public AudioSource collideSound;

    public Event rollingEvent;
    public Event rollEndEvent;
    public Event collideEvent;

    public void OnEnable()
    {
        rollingEvent.callback += OnRoll;
        rollEndEvent.callback += OnExitFloor;
        collideEvent.callback += OnCollide;
    }

    public void OnDisable()
    {
        rollingEvent.callback -= OnRoll;
        rollEndEvent.callback -= OnExitFloor;
        collideEvent.callback -= OnCollide;

    }

    private void OnRoll(object param) // float
    {
        float velocity = (float)param;
        
        rollSound.volume = Mathf.Clamp(Mathf.Log10(velocity) * 0.5f, 0, 1);
        rollSound.pitch = 0.5f + velocity * 0.05f;
    }

    private void OnExitFloor(object param) // void
    {
        rollSound.volume = 0;
    }

    private void OnCollide(object param) // float
    {
        float normalVelocity = (float)param;

        collideSound.volume = Mathf.Clamp(normalVelocity*0.05f, 0, 1);
        collideSound.pitch = 1f + normalVelocity * 0.05f;
        collideSound.Play();
    }
}