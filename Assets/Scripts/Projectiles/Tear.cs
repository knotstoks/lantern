using System.Collections;
using UnityEngine;

public class Tear : MonoBehaviour {
    [SerializeField] private float lifeTime;
    private Rigidbody2D rb;
    private Animator animator;
    private void Start() {
        animator = GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(DeathDelay());
    }
    private IEnumerator DeathDelay() {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
    public void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") { //Damages Player
            other.GetComponent<Player>().SlowPlayer();
            StartCoroutine(Death());
        }
    }
    private IEnumerator Death() {
        animator.SetTrigger("Collide");
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.22f);
        Destroy(gameObject);
    }
}