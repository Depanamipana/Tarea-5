using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour{
    public GameObject target;
    private CharacterController controllerComp;
    public float yTolerance;
    public float distanceToChase;
    public float distanceToAtk = 3f;
    public float speed;
    public float restTime = 1f;
    public float atkRestTime = 3f;
    public float maxSprint = 7f;
    public float minSprint = 5f;
    private float timer = 0f;
    private bool resting = false;
    public Animator animComp;

    private void Awake(){
        controllerComp = GetComponent<CharacterController>();
        if (target == null){
            target = GameObject.Find("Player");
        }
        animComp.SetBool("Walking",true);
    }

    // Update is called once per frame
    void Update(){
        Vector3 origin = Vector3.Scale(transform.position,new Vector3(1f,0f,1f));
        Vector3 end = Vector3.Scale(target.transform.position,new Vector3(1f,0f,1f));
        if (
            (target!=null)&&
            (Vector3.Distance(origin,end) <= distanceToChase)&&
            (Mathf.Abs(transform.position.y - target.transform.position.y) <= yTolerance)
        ){ //chase the target!!
            if (!resting){
                    if (Vector3.Distance(origin,end) <= distanceToAtk){
                        resting = true;
                        timer = atkRestTime;
                        animComp.SetBool("Walking",false);
                        animComp.SetTrigger("ATK");
                    }else{
                        Vector3 moveSpeed = (end - origin).normalized * speed * Time.deltaTime;
                        controllerComp.Move(moveSpeed);
                        if (moveSpeed.magnitude != 0f) {
                            transform.forward = moveSpeed;
                        }
                        timer -= Time.deltaTime;
                        if (timer <= 0f){
                            resting = true;
                            timer = restTime;
                            animComp.SetBool("Walking",false);
                        }
                    }
            }else{
                timer -= Time.deltaTime;
                if (timer <= 0f){
                    timer = Random.Range(minSprint,maxSprint);
                    resting = false;
                    animComp.SetBool("Walking",true);
                }
            }
        }else{
            animComp.SetBool("Walking",false);
        }
    }
}
