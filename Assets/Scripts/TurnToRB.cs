using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnToRB : MonoBehaviour{
    public Rigidbody rb;
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        if (rb == null) {return;}
        if (rb.constraints == RigidbodyConstraints.FreezeAll) {return;}
        if (rb.velocity.magnitude == 0) {return;}
        transform.forward = rb.velocity;

    }
}
