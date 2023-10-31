using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Models;

public class CharacterCont : MonoBehaviour{
    private InputLib input;
    private Vector2 rawMove;
    private Vector2 rawView;
    private float rawSprint;
    public bool windUpDone;
    public bool albert;

    private Vector3 newCameraRotation;
    private Vector3 newPlayerRotation;

    private float fallingSpeed;

    private CharacterController controllerComp;

    [Header ("References")]
    public Transform cameraHolder;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public Animator animComp;
    public GameObject pausePanel;

    [Header ("Settings")]
    public PlayerSettingsModel settings;

    //Shoot vars
    private float holdTimer = 0f;
    private bool charging = false;
    private bool arrowShot = false;
    private GameObject activeBullet;

    //Zoom vars
    private bool zooming = false;
    private Vector3 endPosition;
    private GameObject leftOverBullet;
    private float oldDistance2End;

    private void Awake(){
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        input = new InputLib();

        input.Character.Movement.performed += e => rawMove = e.ReadValue<Vector2>();
        input.Character.View.performed += e => rawView = e.ReadValue<Vector2>();
        input.Character.Jump.performed += e => Jump();
        input.Character.Shoot.performed += e => Shoot(e.ReadValue<float>());
        input.Character.Pause.performed += e => Pause();

        input.Enable();

        newCameraRotation = cameraHolder.localRotation.eulerAngles;
        newPlayerRotation = transform.localRotation.eulerAngles;
        controllerComp = GetComponent<CharacterController>();
    }

    private void FixedUpdate(){
        rawSprint = input.Character.Sprint.ReadValue<float>();
        CalculateView();
        if(!zooming){
            CalcuteMovement();
        }else{
            ZoomMovement();
        }
        ShootTimer();
    }

    private void CalcuteMovement(){
        float forwardSpd;
        float sprint;
        if (rawSprint == 1){
            sprint = settings.sprintFactor;
        }else{
            sprint = 1;
        }
        if (rawMove.y >= 0){
            forwardSpd = settings.forwardSpd * rawMove.y * Time.deltaTime * sprint;
        }else{
            forwardSpd = settings.backwardsSpd *rawMove.y * Time.deltaTime * sprint;
        }
        float horizontalSpd = settings.strafeSpd * rawMove.x * Time.deltaTime * sprint;

        Vector3 newMoveSpeed = new Vector3(horizontalSpd,0,forwardSpd);
        newMoveSpeed = transform.TransformDirection(newMoveSpeed);

        if (fallingSpeed > settings.terminalVelocity){
            fallingSpeed -= settings.gravity * Time.deltaTime;
        }else{
            fallingSpeed = settings.terminalVelocity;
        }

        if(fallingSpeed < -0.01f && controllerComp.isGrounded){
            fallingSpeed = -0.01f;
        }
        newMoveSpeed.y += fallingSpeed;

        controllerComp.Move(newMoveSpeed);
    }

    private void CalculateView(){
        //rawView;
        if (!settings.viewYInverted){
            rawView.y *= -1;
        }
        if (settings.viewXInverted){
            rawView.x *= -1;
        }
        newCameraRotation.x += settings.ViewSens.y * rawView.y * Time.deltaTime;
        newCameraRotation.x = Mathf.Clamp(newCameraRotation.x,settings.yClampMin, settings.yClampMax);

        newPlayerRotation.y += settings.ViewSens.x * rawView.x * Time.deltaTime;

        transform.localRotation = Quaternion.Euler(newPlayerRotation);
        cameraHolder.localRotation = Quaternion.Euler(newCameraRotation);
    }

    //when jump stuff pressed
    private void Jump(){
        //don't jump
        if((!controllerComp.isGrounded) ||(Time.timeScale == 0f)){
            return;
        }else{
            fallingSpeed = settings.jumpForce;
        }
        //jump
    }

    private void ShootTimer(){
        if ((!charging) || (holdTimer == settings.maxHoldTime) || (!windUpDone)) { return;}
        holdTimer += Time.deltaTime;
        animComp.SetFloat("Strength",holdTimer/settings.maxHoldTime);
        if (holdTimer >= settings.maxHoldTime){
            holdTimer = settings.maxHoldTime;
            animComp.SetFloat("Strength",1f);
        }
    }

    private void Shoot(float value){
        if (Time.timeScale == 0f) { return;}
        if (arrowShot && value == 1) { //press when roped
            animComp.SetTrigger("Rope");
            activeBullet.BroadcastMessage("Pull");
            activeBullet = null;
        }else if(arrowShot && value == 0) { //release when roped
            arrowShot = false;
        }else if (value == 1){ //press
            animComp.SetTrigger("Pull");
            holdTimer = 0f;
            charging = true;
        }else if (value == 0){ //release
            animComp.SetTrigger("Release");
            animComp.SetFloat("Strength",0f);
            charging = false;
            GameObject obj = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            Bullet bulletScript = obj.GetComponent<Bullet>();
            bulletScript.force = Mathf.Lerp(settings.minForce , settings.maxForce , holdTimer / settings.maxHoldTime);
            bulletScript.ropeOrigin = bulletSpawnPoint;
            arrowShot = true;
            activeBullet = obj;
        }
    }

    //when you pull on a static wall and get flinged towards it
    public void PullZoom(Vector3 endPoint, GameObject bullet){
        zooming = true;
        endPosition = endPoint;
        leftOverBullet = bullet;
        oldDistance2End = Mathf.Infinity;
    }

    private void ZoomMovement(){
        Vector3 newMoveSpeed = ((endPosition - transform.position).normalized) * Time.deltaTime * settings.pullVelocity;
        controllerComp.Move(newMoveSpeed);
        float newDistance = Vector3.Distance(transform.position,endPosition);
        if (oldDistance2End - newDistance <= settings.zoomTolerance){
            zooming = false;
            Destroy(leftOverBullet);
        }
        oldDistance2End = newDistance;
    }

    public void Pause(){
        if (Time.timeScale == 1f){
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else{
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void OnTriggerEnter(Collider coll){
        if (coll.tag == "Albert"){
            albert = true;
            coll.gameObject.SetActive(false);   
        }
    }
}
