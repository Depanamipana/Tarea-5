using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PullableEvent : MonoBehaviour{
    public UnityEvent pullEvent;
    public void Pull(){
        pullEvent?.Invoke();
    }
}
