using System.Collections;
using UnityEngine;

public class HomingMissle : MonoBehaviour {
    [SerializeField] private float lifeTime;
    [SerializeField] private float speed;
    private Transform target;
    private Rigidbody2D rb;
    private Animator animator;
    private void Start() {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(DeathDelay());
    }
    private void FixedUpdate() {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
    private IEnumerator DeathDelay() {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
    public void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") { //Damages Player
            other.GetComponent<Enemy>().Damage(1);
            StartCoroutine(Death());
        }

        if (other.tag == "Invincible") { //Gets Destroyed when it hits something Invincible (eg. shields or walls)
            StartCoroutine(Death());
        }
    }
    private IEnumerator Death() {
        rb.velocity = Vector2.zero;
        animator.SetTrigger("Collide");
        yield return new WaitForSeconds(0.22f);
        Destroy(gameObject);
    }
}
