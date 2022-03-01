using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public RollEvent rollEvent;
    public CollideEvent collideEvent;
    public Transform pivot;


    private Rigidbody rigid;

    private void Awake()
    {
        this.rigid = this.GetComponent<Rigidbody>();
    }

    private void OnCollisionStay(Collision coll)
    {
        if(coll.transform.tag == "Floor")
        {
            rollEvent.OnRoll?.Invoke(rigid.velocity.magnitude);
        }
    }
    private void OnCollisionExit(Collision coll)
    {
        if(coll.transform.tag == "Floor")
        {
            rollEvent.OnExitFloor?.Invoke();
        }
    }
    private void OnCollisionEnter(Collision coll)
    {
        if(coll.transform.tag == "Wall")
        {
            Vector3 normal = Vector3.zero;
            foreach(ContactPoint c in coll.contacts)
            {
                normal += c.normal;
            }
            Vector3.Normalize(normal);

            QuantizeNormal(ref normal);

            float normalVelocity = Mathf.Abs(Vector3.Dot(rigid.velocity, normal));

            collideEvent.OnCollide?.Invoke(normalVelocity);
        }
    }

    private void QuantizeNormal(ref Vector3 normal)
    {
        float accuValue = 0.4f;

        if (Mathf.Abs((pivot.right - normal).magnitude) < accuValue)
        {
            normal = pivot.right;
            return;
        }
        else if (Mathf.Abs((-pivot.right - normal).magnitude) < accuValue)
        {
            normal = -pivot.right;
            return;
        }
        else if(Mathf.Abs((pivot.forward - normal).magnitude) < accuValue)
        {
            normal = pivot.forward;
            return;
        }
        else if (Mathf.Abs((-pivot.forward - normal).magnitude) < accuValue)
        {
            normal = -pivot.forward;
            return;
        }
    }
}
