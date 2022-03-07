using UnityEngine;

public class MotionEffectController : MonoBehaviour
{
    public AudioSource rollSound;
    public AudioSource collideSound;

    public RollEvent rollEvent;
    public CollideEvent collideEvent;

    public void OnEnable()
    {
        rollEvent.OnRoll += OnRoll;
        rollEvent.OnExitFloor += OnExitFloor;
        collideEvent.OnCollide += OnCollide;
    }

    public void OnDisable()
    {
        rollEvent.OnRoll -= OnRoll;
        rollEvent.OnExitFloor -= OnExitFloor;
        collideEvent.OnCollide -= OnCollide;

    }

    private void OnRoll(float velocity)
    {
        rollSound.volume = Mathf.Clamp(Mathf.Log10(velocity) * 0.5f, 0, 1);
        rollSound.pitch = 0.5f + velocity * 0.05f;
    }

    private void OnExitFloor()
    {
        rollSound.volume = 0;
    }

    private void OnCollide(float normalVelocity)
    {
        collideSound.volume = Mathf.Clamp(normalVelocity*0.05f, 0, 1);
        collideSound.pitch = 1f + normalVelocity * 0.05f;
        collideSound.Play();
    }
}