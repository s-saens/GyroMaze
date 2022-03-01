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


    // For Test. TODO : delete this.
    public void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            rigid.AddForce(Vector3.forward*1000);
        }
        if(Input.GetKey(KeyCode.S))
        {
            rigid.AddForce(Vector3.back*1000);
        }
        if(Input.GetKey(KeyCode.A))
        {
            rigid.AddForce(Vector3.left*1000);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigid.AddForce(Vector3.right*1000);
        }
    }

    private void OnCollisionStay(Collision coll)
    {
        if(coll.transform.tag == "Floor")
        {
            rollEvent.OnRoll?.Invoke(rigid.velocity.magnitude);
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

            Debug.Log(normal);

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
        Debug.Log("not changed");
    }
}
