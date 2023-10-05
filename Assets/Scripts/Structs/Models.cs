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
        public float forwardSpd;
        public float strafeSpd;
        public float backwardsSpd;
    }
    #endregion
}
