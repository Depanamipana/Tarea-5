using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force : MonoBehaviour{
    public Rigidbody rb;
    public Vector3 force;
    public float magnitude;

    public void ApplyForce(){
        rb.constraints = RigidbodyConstraints.None;
        rb.AddForce(force.normalized * magnitude);
    }
}
