using System.Collections;
using UnityEngine;

public class CandleChest : Enemy { 
    private Transform target;
    [SerializeField] private float resetTime;
    [SerializeField] private GameObject fireBall;
    [SerializeField] private float fireBallSpeed;
    [SerializeField] private Animator animator;
    private float fireTime;
    private bool canMove;
    private Rigidbody2D rb;
    private Vector2[] shootDirection = {
        new Vector2(0, 1), //Up
        new Vector2(0, -1), //Down
        new Vector2(-1, 0), //Left
        new Vector2(1, 0) //Right
    };
    private void Start() {
        fireTime = resetTime;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        canMove = true;
    }

    private void Update() {
        if (health <= 0) {
            GameObject.FindGameObjectWithTag("DungeonSceneManager").GetComponent<Manager1>().EnemiesNow(-1);
            Destroy(gameObject);
        }

        if (fireTime > 0) {
            fireTime -= Time.deltaTime;
        } else {
            fireTime = resetTime;
            float x = transform.position.x - target.position.x;
            float y = transform.position.y - target.position.y;
            if (x < y) {
                if (x < 0) {
                    StartCoroutine(Flamethrower(3));
                    canMove = false;   
                } else {
                    StartCoroutine(Flamethrower(1));
                    canMove = false;
                }
            } else {
                if (y < 0) {
                    StartCoroutine(Flamethrower(0));
                    canMove = false;
                } else {
                    StartCoroutine(Flamethrower(2));
                    canMove = false;
                }
            }
        }
    }
    private void FixedUpdate() {
        if (canMove && Vector2.Distance(transform.position, target.position) > 0) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        animator.SetFloat("Hori", target.position.x - transform.position.x);
        animator.SetFloat("Vert", target.position.y - transform.position.y);
    }
    private IEnumerator Flamethrower(int direction) {
        //Fire the flamethrower
        GameObject projectile = Instantiate(fireBall, transform.position, Quaternion.identity) as GameObject;
        projectile.GetComponent<Rigidbody2D>().velocity = shootDirection[direction] * fireBallSpeed;
        yield return new WaitForSeconds(1);
        canMove = true;
    }
}
