using System;
using System.Collections.Generic;
using UnityEngine;

public static class Models{
    #region - Player
    [Serializable]
    public class PlayerSettingsModel{
        [Header("Camera Settings")]
        public Vector2 ViewSens;

        public bool viewXInverted;
        public bool viewYInverted;

        public float yClampMax;
        public float yClampMin;
        [Header("Movement Speeds")]
        public float sprintFactor;
        public float forwardSpd;
        public float strafeSpd;
        public float backwardsSpd;
        [Header("Gravity")]
        public float terminalVelocity;
        public float gravity;
        [Header("Jump")]
        public float jumpForce;

        [Header("Shoot Settings")]
        public float minForce;
        public float maxForce;
        public float maxHoldTime; //in seconds

        [Header("Pull")]
        public float pullVelocity;
        public float zoomTolerance; //the maximum ammount of movement during a pull for it to be considered over
    }
    #endregion
}
