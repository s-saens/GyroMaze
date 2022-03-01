using UnityEngine;

public class MEController : MonoBehaviour
{
    public AudioSource rollSound;
    public AudioSource collideSound;

    public RollEvent rollEvent;
    public CollideEvent collideEvent;

    public void OnEnable()
    {
        rollEvent.OnRoll += OnRoll;
        collideEvent.OnCollide += OnCollide;
    }

    public void OnDisable()
    {
        rollEvent.OnRoll -= OnRoll;
        collideEvent.OnCollide -= OnCollide;

    }

    private void OnRoll(float velocity)
    {
        rollSound.volume = Mathf.Log10(velocity) * 0.5f;
        rollSound.pitch = 0.5f + velocity * 0.05f;
    }
    private void OnCollide(float normalVelocity)
    {
        collideSound.volume = Mathf.Pow(normalVelocity, 2)*0.01f;
        collideSound.pitch = 1f + normalVelocity * 0.05f;
        collideSound.Play();
    }
}
