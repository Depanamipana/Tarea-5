using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Models;

public class CharacterCont : MonoBehaviour{
    private InputLib input;
    public Vector2 rawMove;
    public Vector2 rawView;

    private Vector3 newCameraRotation;
    private Vector3 newPlayerRotation;

    private float fallingSpeed;

    private CharacterController controllerComp;

    [Header ("References")]
    public Transform cameraHolder;

    [Header ("Settings")]
    public PlayerSettingsModel settings;

    private void Awake(){
        input = new InputLib();

        input.Character.Movement.performed += e => rawMove = e.ReadValue<Vector2>();
        input.Character.View.performed += e => rawView = e.ReadValue<Vector2>();
        input.Character.Jump.performed += e => Jump();

        input.Enable();

        newCameraRotation = cameraHolder.localRotation.eulerAngles;
        newPlayerRotation = transform.localRotation.eulerAngles;
        controllerComp = GetComponent<CharacterController>();
    }

    private void Update(){
        CalculateView();
        CalcuteMovement();
    }

    private void CalcuteMovement(){
        float forwardSpd;
        if (rawMove.y >= 0){
            forwardSpd = settings.forwardSpd * rawMove.y * Time.deltaTime;
        }else{
            forwardSpd = settings.backwardsSpd *rawMove.y * Time.deltaTime;
        }
        float horizontalSpd = settings.strafeSpd * rawMove.x * Time.deltaTime;

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
        if(!controllerComp.isGrounded){
            return;
        }else{
            fallingSpeed = settings.jumpForce;
        }
        //jump
    }
}
