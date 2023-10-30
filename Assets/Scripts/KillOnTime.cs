using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnTime : MonoBehaviour{
    public bool killing = false;
    public float timer = 1f;
    // Update is called once per frame
    void Update(){
        if (killing){
            timer -= Time.deltaTime;
            if (timer <= 0f){
                Destroy(gameObject);
            }
        }
    }

    public void Kill(){
        killing = true;
    }
}
