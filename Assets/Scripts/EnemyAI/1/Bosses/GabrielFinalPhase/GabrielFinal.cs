using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Animation References
Trigger: Idle - Flaps his wings at the top of the scene
Trigger: Imprisoned - Imprisoned in the middle of the map

*/
public class GabrielFinal : MonoBehaviour {
    public int health; //40
    private bool dead;
    private GabrielFinalRoom sceneManager;
    private void Start() {
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<GabrielFinalRoom>();
    }
    private void Update() {
        if (health <= 0 && !dead) {
            dead = true;
            sceneManager.FinishFight();
            //Animation for Gabriel Second form dying

        }
    }
    //Code for all the Feather Patterns
}