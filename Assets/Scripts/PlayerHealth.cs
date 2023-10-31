using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour{
    public float maxHealth = 100f;
    //[HideInInspector]
    public float health;
    public TMP_Text toUpdate;
    public UnityEvent OnDamageTaken;
    public UnityEvent OnDeath;

    void Start(){
        health = maxHealth;
    }

    void Update(){
        toUpdate.text = health.ToString();
    }

    void OnTriggerEnter(Collider coll){
        if (coll.tag == "Damage"){
            takeDamage(FindParent(coll.gameObject,"Enemy").transform.position,15f);
        }
    }

    GameObject FindParent(GameObject obj, string tag){
        bool found = false;
        bool checking = obj;
        while(!found){
            if (obj.tag != tag && obj.transform.parent != null){
                obj = obj.transform.parent.gameObject;
            }else{
                found = true;
            }
        }
        return obj;
    }

    public void takeDamage(Vector3 source, float damage){
        health -= damage;
        OnDamageTaken?.Invoke();
        if (health <= 0){
            Kill();
        }
    }

    public void Kill(){
        OnDeath?.Invoke();
    }
}
