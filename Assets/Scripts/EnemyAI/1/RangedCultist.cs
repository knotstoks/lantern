using System.Collections;
using UnityEngine;

public class RangedCultist : Enemy {
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject cultistArrow;
    [SerializeField] private float arrowSpeed;
    [SerializeField] private float shootResetTime;
    private Transform target;
    private Rigidbody2D rb;
    private float shootTime;
    private bool shooting;
    private bool died;
    private void Start() {
        GetSprite();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        died = false;
    }
    private void Update() {
        if (health <= 0 && !died) {
            died = true;
            damage = 0;
            StartCoroutine(Death());
        }

        //Shoot the Player after the interval
        if (shootTime > 0) {
            shootTime -= Time.deltaTime;
        } else {
            shooting = true;
            StartCoroutine(Shoot(target.position));
            shootTime = shootResetTime;
        }
    }
    private void FixedUpdate() {
        if (!shooting && (transform.position - target.position).sqrMagnitude < 12) {
            rb.velocity = (transform.position - target.position).normalized * speed;
        } else {
            rb.velocity = new Vector2(0, 0);
        }

        if (rb.velocity.magnitude != 0) {
            animator.SetFloat("Hori", rb.velocity.x);
            animator.SetFloat("Vert", rb.velocity.y);
        } else {
            animator.SetFloat("Hori", target.position.x - transform.position.x);
            animator.SetFloat("Vert", target.position.y - transform.position.y);
        }
    }
   private IEnumerator Shoot(Vector2 aim) {
        animator.SetBool("Shooting", true);
        animator.SetFloat("ShootHori", aim.x - transform.position.x);
        animator.SetFloat("ShootVert", aim.y - transform.position.y);
        //Wait for the animation to play out then fire arrow
        yield return new WaitForSeconds(1);
        animator.SetBool("Shooting", false);
        shooting = false;
        GameObject arrow = Instantiate(cultistArrow, transform.position, transform.rotation) as GameObject;
        arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(aim.x - transform.position.x, aim.y - transform.position.y);
    }
    private IEnumerator Death() {
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(0.6f);
        GameObject.FindGameObjectWithTag("DungeonSceneManager").GetComponent<Manager1>().EnemiesNow(-1);
        Destroy(gameObject);
    }
}