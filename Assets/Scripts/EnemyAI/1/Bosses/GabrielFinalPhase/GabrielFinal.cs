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
    private bool patternEnded;
    private bool canDamage;
    private SpriteRenderer spriteRenderer;
    private GabrielFinalRoom sceneManager;
    private void Start() {
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<GabrielFinalRoom>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        canDamage = false;
    }
    private void Update() {
        if (health <= 0 && !dead) {
            dead = true;
            sceneManager.FinishFight();
            //Animation for Gabriel Second form dying

        }
    }
    public void Imprison() {
        canDamage = true;
        //Teleport Gabriel to center and imprison
        //EDIT!!!!!!!!!!
    }
    public void Return() {
        canDamage = false;
        //Teleport Gabriel back
        //EDIT!!!!!!!!!!!
    }
    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Player>().Damage(1);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (canDamage && other.tag == "Bullet") {
            Rigidbody2D bullet = other.GetComponent<Rigidbody2D>();
            StartCoroutine(FlashRed());
        }
    }
    private IEnumerator FlashRed() {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }
    //Code for all the Feather Patterns
    private void StartFeatherPattern() {

    }
}