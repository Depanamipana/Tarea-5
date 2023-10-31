using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTouchEvent : MonoBehaviour{
    public string toLoad;
    public void Load(){
        SceneManager.LoadScene(toLoad);
    }
}
