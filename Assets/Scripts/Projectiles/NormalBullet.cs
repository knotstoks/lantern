using System.Collections;
using UnityEngine;

public class NormalBullet : MonoBehaviour {
    [SerializeField] private float lifeTime;
    [SerializeField] private Animator animator;
    private Rigidbody2D rb;
    private IEnumerator Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        float hori = rb.velocity.x;
        float vert = rb.velocity.y;
        animator.SetFloat("BHori", hori);
        animator.SetFloat("BVert", vert);
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
    public void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") { //Damages Enemies
            other.GetComponent<Enemy>().Damage(1);
            StartCoroutine(Collided());
        }

        if (other.tag == "Boss") { //Damages Bosses
            other.GetComponent<Boss>().Damage(1);
            StartCoroutine(Collided());
        }

        if (other.tag == "Arm") { //Damages Four Arms
            other.GetComponent<Arm>().Damage(1);
            StartCoroutine(Collided());
        }

        if (other.tag == "Invincible") { //Gets Destroyed when it hits something Invincible (eg. shields or walls)
            StartCoroutine(Collided());
        }
    }
    private IEnumerator Collided() {
        animator.SetTrigger("Collide");
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.22f);
        Destroy(gameObject);
    }
}