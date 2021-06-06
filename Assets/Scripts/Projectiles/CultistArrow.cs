using System.Collections;
using UnityEngine;

public class CultistArrow : MonoBehaviour {
    [SerializeField] private float lifeTime;
    [SerializeField] private Animator animator;
    private Rigidbody2D rb;

    private IEnumerator Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(DeathDelay());

        yield return new WaitForSeconds(0.01f);
        float hori = rb.velocity.x;
        float vert = rb.velocity.y;

        animator.SetFloat("BHori", hori);
        animator.SetFloat("BVert", vert);
    }

    private IEnumerator DeathDelay() {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") { //Damages Player
            other.GetComponent<Player>().Damage(1);
            StartCoroutine(Death());
        }

        if (other.tag == "Invincible") { //Gets Destroyed when it hits something Invincible (eg. shields or walls)
            StartCoroutine(Death());
        }
    }
    private IEnumerator Death() {
        rb.velocity = Vector2.zero;
        animator.SetTrigger("Collide");
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}