using System.Collections;
using UnityEngine;

/**
Animation References:
Float: Hori, Vert - Controls the direction Gabriel is facing

Trigger: Start - nothing...will stand in my way (spreads wings)
Trigger: Walking - Walks to a random position on the map using blend trees

Trigger: Dash - Dash Attack
Trigger: HomingShots - Homing Bullets attack
Trigger: Feathers - Feather attack
**/

public class Gabriel : Boss {
    [SerializeField] private float minX, maxX, minY, maxY;
    [SerializeField] private GameObject homingBullet;
    [SerializeField] private GameObject feather;
    [SerializeField] private float featherSpeed;
    [SerializeField] private float specialAttackCooldown;
    [SerializeField] private float moveResetTime;
    private float specialAttackTime; // 1 for dash, 2 for homing, 3 for feathers
    private bool notAttacking;
    private Transform target;
    private GabrielBossRoom gabrielBossRoom;
    private Animator animator;
    private int lastAttack;
    private Vector2 pos;
    private float moveTime;
    public bool start;
    private void Start() {
        Initialize();
        notAttacking = true;
        start = false;
        slider.minValue = 0;
        slider.maxValue = health;
        specialAttackTime = -1f;
        moveTime = -1f;
        start = false;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        gabrielBossRoom = GameObject.FindGameObjectWithTag("DungeonSceneManager").GetComponent<GabrielBossRoom>();
        animator = GetComponent<Animator>();
        animator.SetInteger("Attack", 0);
        lastAttack = -1;
    }
    public void StartBossBattle() {
        start = true;
        moveTime = moveResetTime;
        specialAttackTime = specialAttackCooldown;
    }
    private void Update() {
        if (start) {
            //Attacks Randomly
            if (specialAttackTime < 0) {
                DoRandomAttack();
                notAttacking = false;
            } else {
                specialAttackTime -= Time.deltaTime;
            }

            //Moves Randomly
            if (notAttacking) {
                if (moveTime > 0) {
                    if (((Vector2) transform.position - pos).magnitude < 0.01) {
                        moveTime -= Time.deltaTime;
                    } else {
                        transform.position = Vector2.MoveTowards(transform.position, pos, speed * Time.deltaTime);
                    }
                } else {
                    pos.x = Random.Range(minX, maxX);
                    pos.y = Random.Range(minY, maxY);
                }
            }

            //Looks towards direction of travel until reaches then looks at player
            if (notAttacking && ((Vector2) transform.position - pos).magnitude < 0.01) {
                animator.SetFloat("Hori", target.position.x - transform.position.x);
                animator.SetFloat("Vert", target.position.y - transform.position.y);
            } else {
                animator.SetFloat("Hori", pos.x - transform.position.x);
                animator.SetFloat("Vert", pos.y - transform.position.y);
            }
        }
    }
    private void DoRandomAttack() {
        if (start) {
            int rand = Random.Range(1, 4);
            while (rand == lastAttack) { //Don't trigger the same attack twice
                rand = Random.Range(1, 4);
            }
            lastAttack = rand;
            if (rand == 1) {
                StartCoroutine(Dash());
            } else if (rand == 2) {
                StartCoroutine(HomingShots());
            } else {
                StartCoroutine(Feathers());
            }
        }
    }
    private IEnumerator Dash() {
        //Charge up Dash
        yield return new WaitForSeconds(0f);
        //Dashes
        yield return new WaitForSeconds(0f);
        //Resets to normal
        specialAttackTime = specialAttackCooldown;
        notAttacking = true;
    }
    private IEnumerator HomingShots() {
        //Charge up Homing Shots
        yield return new WaitForSeconds(0f);
    }
    private IEnumerator Feathers() {
        //Animation for feathers - shoots 3 times
        for (int i = 0; i < 3; i++) {
            ShootFeathers(target.position);
            yield return new WaitForSeconds(0f);
        }
    }
    private void ShootFeathers(Vector2 aim) {

    }
}