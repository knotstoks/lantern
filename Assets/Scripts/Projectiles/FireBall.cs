using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {
    [SerializeField] private Animator animator;
    [SerializeField] private float lifeTime;
    private Rigidbody2D rb;
    private IEnumerator Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(DeathDelay());
        yield return new WaitForSeconds(0.8f);
        animator.SetBool("StartUp", true);
    }
    private void FixedUpdate() {
        animator.SetFloat("ShootHori", rb.velocity.x);
        animator.SetFloat("ShootVert", rb.velocity.y);
    }
    private IEnumerator DeathDelay() {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
    public void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") { //Damages Player
            other.GetComponent<Player>().Damage(2);
            Destroy(gameObject);
        }

        if (other.tag == "Invincible") { //Gets Destroyed when it hits something Invincible (eg. shields or walls)
            Destroy(gameObject);
        }
    }
}