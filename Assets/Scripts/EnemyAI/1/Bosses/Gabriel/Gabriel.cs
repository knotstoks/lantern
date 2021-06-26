using System.Collections;
using UnityEngine;

/**
Animation References:
Float: Hori, Vert - Controls the direction Gabriel is facing

Trigger: Start - nothing...will stand in my way (spreads wings)
Trigger: Walking - Walks to a random position on the map using blend trees

Trigger: Dash - Dash Attack (1)
Trigger: HomingShots - Homing Bullets attack (2)
Trigger: Feathers - Feather attack (3)

Trigger: Death - Plays the death animation
**/

public class Gabriel : Boss {
    [SerializeField] private float minX, maxX, minY, maxY;
    [SerializeField] private GameObject homingBullet;
    [SerializeField] private GameObject feather;
    [SerializeField] private float featherSpeed;
    [SerializeField] private float specialAttackCooldown;
    [SerializeField] private float moveResetTime;
    [SerializeField] private GameObject downedGabriel;
    private float specialAttackTime; // 1 for dash, 2 for homing, 3 for feathers
    private bool notAttacking;
    private Transform playerTarget;
    private Vector2 randomTarget;
    private GabrielBossRoom gabrielBossRoom;
    private Animator animator;
    private int lastAttack;
    private Vector2 pos;
    private float moveTime;
    private bool dashing;
    private bool feathering;
    private bool start;
    private void Start() {
        Initialize();
        notAttacking = true;
        start = false;
        dashing = false;
        feathering = false;
        slider.minValue = 0;
        slider.maxValue = health;
        specialAttackTime = -1f;
        moveTime = -1f;
        start = false;
        playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        gabrielBossRoom = GameObject.FindGameObjectWithTag("DungeonSceneManager").GetComponent<GabrielBossRoom>();
        animator = GetComponent<Animator>();
        lastAttack = -1;
    }
    public void StartBossBattle() {
        start = true;
        moveTime = moveResetTime;
        specialAttackTime = specialAttackCooldown;
        animator.SetTrigger("Walking");
    }
    private void Update() {
        if (health <= 0 && start) {
            start = false;
            StartCoroutine(Collapse());
            gabrielBossRoom.CompleteFight();
        }

        if (start) {
            //Attacks Randomly
            if (specialAttackTime < 0 && notAttacking) {
                DoRandomAttack();
                notAttacking = false;
                specialAttackTime = specialAttackCooldown;
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
                    moveTime = moveResetTime;
                }
            }

            if (dashing) {
                transform.position = Vector2.MoveTowards(transform.position, playerTarget.position, 3 * Time.deltaTime);
            }

            //For Animations
            if (notAttacking && ((Vector2) transform.position - pos).magnitude > 0.01) {
                animator.SetFloat("Hori", pos.x - transform.position.x);
                animator.SetFloat("Vert", pos.y - transform.position.y);
            } else if (dashing || feathering) {
                animator.SetFloat("Hori", playerTarget.position.x - transform.position.x);
                animator.SetFloat("Vert", playerTarget.position.y - transform.position.y);
            } else {
                animator.SetFloat("Hori", playerTarget.position.x - transform.position.x);
                animator.SetFloat("Vert", playerTarget.position.y - transform.position.y);
            }
        }
    }
    private void DoRandomAttack() {
        notAttacking = false;
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
        animator.SetTrigger("Dash");
        yield return new WaitForSeconds(0.05f);
        //Dashes
        dashing = true;
        yield return new WaitForSeconds(1.4f);
        //Resets to normal
        dashing = false;
        animator.SetTrigger("Walking");
        specialAttackTime = specialAttackCooldown;
        notAttacking = true;
    }
    private IEnumerator HomingShots() {
        //Charge up Homing Shots
        animator.SetTrigger("HomingShots");
        yield return new WaitForSeconds(1.5f);
        ShootHomingShot();
        yield return new WaitForSeconds(0.9f);
        animator.SetTrigger("Walking");
        notAttacking = true;
    }
    private IEnumerator Feathers() {
        feathering = true;
        animator.SetTrigger("Feathers");
        //Animation for feathers - shoots 3 times
        for (int i = 0; i < 3; i++) {
            ShootFeathers(playerTarget.position);
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.2f);
        animator.SetTrigger("Walking");
        notAttacking = true;
    }
    private void ShootHomingShot() {
        Instantiate(homingBullet, transform.position,Quaternion.identity);
    }
    private void ShootFeathers(Vector2 aim) {
        GameObject projectile = Instantiate(feather, transform.position, Quaternion.identity) as GameObject;
        projectile.GetComponent<Feather>().endPosition = aim;
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(aim.x - transform.position.x, aim.y - transform.position.y).normalized * featherSpeed;
    }
    //For the Intro Scene
    public IEnumerator TurnAround() {
        animator.SetTrigger("TurnAround");
        yield return new WaitForSeconds(0.8f);
        animator.SetTrigger("FacingFront");
    }
    public IEnumerator WingsEmerge() {
        animator.SetTrigger("WingsEmerge");      
        yield return new WaitForSeconds(0.75f);
        animator.SetTrigger("WingsIdle");
    }
    private IEnumerator Collapse() {
        //Teleport the player and Gabriel to specific positions
        playerTarget.gameObject.transform.position = new Vector2(0, -2);
        transform.position = new Vector2(0, 3);
        yield return new WaitForSeconds(0.2f);
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(1.25f);
        animator.SetTrigger("DeathIdle");
        Instantiate(downedGabriel, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}