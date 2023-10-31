using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour{
    public GameObject target;
    public CharacterController controllerComp;
    public float speed;
    // Start is called before the first frame update
    void Start(){
        if (target == null){
            target = GameObject.Find("Player");
        }    
    }

    // Update is called once per frame
    void Update(){
        Vector3 newMoveSpeed;
        Vector3 start = Vector3.Scale(transform.position,new Vector3(1f,0f,1f));
        Vector3 end = Vector3.Scale(target.transform.position,new Vector3(1f,0f,1f));
        newMoveSpeed = end - start;
        transform.forward = newMoveSpeed.normalized;
        newMoveSpeed.y = -5;
        controllerComp.Move(newMoveSpeed * Time.deltaTime * speed);
    }
}
