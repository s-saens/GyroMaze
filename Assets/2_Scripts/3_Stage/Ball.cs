using UnityEngine;
using Newtonsoft.Json;

public class Ball : MonoBehaviour
{
    public Event rollingEvent;
    public Event rollEndEvent;
    public Event collideEvent;

    private Rigidbody rigid;
    private Vector3 velocity;

    private void Awake()
    {
        this.rigid = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        velocity = rigid.velocity;
        if(Input.GetKey(KeyCode.W))
        {
            rigid.AddForce(Vector3.forward * 1000);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rigid.AddForce(Vector3.back * 1000);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rigid.AddForce(Vector3.left * 1000);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigid.AddForce(Vector3.right * 1000);
        }
    }

    private void OnCollisionStay(Collision coll)
    {
        rollingEvent.Invoke(rigid.velocity.magnitude * Time.deltaTime * 60);
    }
    private void OnCollisionExit(Collision coll)
    {
        rollEndEvent.Invoke("");
    }
    private void OnCollisionEnter(Collision coll)
    {
        Vector3 normal = Vector3.zero;
        foreach(ContactPoint c in coll.contacts)
        {
            normal += c.normal;
        }
        Vector3.Normalize(normal);

        QuantizeNormal(ref normal);

        float normalVelocity = Mathf.Abs(Vector3.Dot(velocity, normal));


        collideEvent.Invoke(normalVelocity.ToString());
    }

    private void QuantizeNormal(ref Vector3 normal)
    {
        float accuValue = 0.4f;

        if (Mathf.Abs((Vector3.right - normal).magnitude) < accuValue)
        {
            normal = Vector3.right;
            return;
        }
        else if (Mathf.Abs((-Vector3.right - normal).magnitude) < accuValue)
        {
            normal = -Vector3.right;
            return;
        }
        else if(Mathf.Abs((Vector3.forward - normal).magnitude) < accuValue)
        {
            normal = Vector3.forward;
            return;
        }
        else if (Mathf.Abs((-Vector3.forward - normal).magnitude) < accuValue)
        {
            normal = -Vector3.forward;
            return;
        }
    }
}
