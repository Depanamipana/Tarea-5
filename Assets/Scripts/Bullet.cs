using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{



    [Header("Settings")]
    public float Life = 0.1f;
    public float force;

    [Header("Reference")]
    public GameObject ropePrefab;
    [HideInInspector]
    public Transform ropeOrigin;

    public bool stuck = false;
    public PullableEvent toPull;
    void Start(){
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * force);
        GameObject rope = CreateCylinderBetweenPoints(transform.position, ropeOrigin.position, 0.1f);
        rope.GetComponent<Rope>().startPoint = transform;
        rope.GetComponent<Rope>().endPoint = ropeOrigin;
    }

    void Update(){
        if (!stuck) { 
            Life -= Time.deltaTime;
            if (Life <= 0){
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision coll){
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        stuck = true;
        if (coll.gameObject.GetComponent<PullableEvent>() != null){
            toPull = coll.gameObject.GetComponent<PullableEvent>();
        }
    }

    GameObject CreateCylinderBetweenPoints(Vector3 start,Vector3 end,float width){
        var offset = end - start;
        var scale = new Vector3(width, offset.magnitude / 2.0f, width);
        var position = start + (offset / 2.0f);

        var cylinder = Instantiate(ropePrefab, position, Quaternion.identity);
        cylinder.transform.up = offset;
        cylinder.transform.localScale = scale;
        return cylinder;
    }

    void Pull() {
        if (toPull != null){
            toPull.Pull();
        }else{
            Debug.Log("I HABE BEEN PULLED ONHO");
        }
        Destroy(gameObject);
    }
}
