using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DisableEvent : MonoBehaviour{
    public UnityEvent disable;
    void OnDisable() {
        disable?.Invoke();
    }

}
