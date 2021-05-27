using System.Collections;
using UnityEngine;

public class GolemCandling : Enemy {
    [SerializeField] private Animator animator;
    [SerializeField] private bool inIntro;
    [SerializeField] private float tempSpeed;
    private Transform target;
    private Rigidbody2D rb;
    private Vector2 move;
    private bool died;
    private void Start() {
        if (inIntro) {
            StopMoving();
        }
        GetSprite();
        rb = gameObject.GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        died = false;
    }
    private void Update() {
        if (health <= 0 && !died) {
            died = true;
            damage = 0;
            StartCoroutine(Death());
        }
    }
    private void FixedUpdate() {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        animator.SetFloat("Hori", target.position.x - transform.position.x);
        animator.SetFloat("Vert", target.position.y - transform.position.y);
    }
    private IEnumerator Death() {
        if (inIntro) {
            GameObject.FindGameObjectWithTag("SceneManager").GetComponent<WaxDungeonIntro>().KillCandling();
        }
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
    }
    private void StopMoving() {
        speed = 0;
    }
    public void MoveAgain() {
        speed = tempSpeed;
    }
}
