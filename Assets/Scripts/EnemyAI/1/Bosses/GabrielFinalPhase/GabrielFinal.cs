using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Animation References
Trigger: Idle - Flaps his wings at the top of the scene
Trigger: Imprisoned - Imprisoned in the middle of the map
Trigger: Death - Gabriel Dies
*/
public class GabrielFinal : MonoBehaviour {
    public int health; //40
    private bool dead;
    private bool patternEnded;
    private bool canDamage;
    private SpriteRenderer spriteRenderer;
    private GabrielFinalRoom sceneManager;
    private Animator animator;
    private void Start() {
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<GabrielFinalRoom>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        canDamage = false;
    }
    private void Update() {
        if (health <= 0 && !dead) {
            Return();
            dead = true;
            sceneManager.FinishFight();
            //Animation for Gabriel Second form dying
            animator.SetTrigger("Death");
        }
    }
    public void Imprison() {
        canDamage = true;
        //Teleport Gabriel to center and imprison
        transform.position = new Vector2(0.57f, 8f);
        animator.SetTrigger("Imprisoned");
    }
    public void Return() {
        if (!dead) {
            canDamage = false;
            //Teleport Gabriel back
            transform.position = new Vector2(0.57f, 9.08f);
            animator.SetTrigger("Idle");
        }
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