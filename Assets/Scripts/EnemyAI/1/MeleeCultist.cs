using UnityEngine;

//TODO: Sprite, Animation THIS IS XIN YAN'S PROBLEM CHILD
public class MeleeCultist : Enemy {
    private Transform target;
    [SerializeField] private Animator animator;
    private float attackTime;
    private void Start() {
        GetSprite();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update() {
        if (health <= 0) {
            GameObject.FindGameObjectWithTag("DungeonSceneManager").GetComponent<Manager1>().EnemiesNow(-1);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate() {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        animator.SetFloat("Hori", target.position.x - transform.position.x);
        animator.SetFloat("Vert", target.position.y - transform.position.y);
    }
}