using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour{
    public float maxHealth = 100f;
    //[HideInInspector]
    public float health;
    public TMP_Text toUpdate;

    void Start(){
        health = maxHealth;
    }

    void Update(){
        toUpdate.text = health.ToString();
    }

    public void takeDamage(Vector3 source, float damage){

    }
}
