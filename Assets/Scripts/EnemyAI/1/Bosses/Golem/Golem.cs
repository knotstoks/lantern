using System.Collections;
using UnityEngine;

/**
Animation References:
Trigger: Start - Starts boss fight
Trigger: Walking - Goes back to moving towards the player
Int: Attack - 0: Stay on Walking
              1: Charge
              2: Bullet
              3: Spawn Candling
Trigger: Death - Ends boss fight

Float: Hori - movement horizontally
Float: Vert - movement vertically
**/

public class Golem : Boss {
    [SerializeField] private float resetAttackTime;
    [SerializeField] private float restTime;
    [SerializeField] private GameObject bloodBullet;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] sounds; //0 for footsteps, 1 for shooting, 2 for death
    [SerializeField] private GameObject candling;
    [SerializeField] private GameObject puddle;
    public bool start;
    private Transform target;
    private GolemBossRoom golemBossRoom;
    private float attackTime;
    private Animator animator;
    private bool notAttacking;
    private bool charging;
    private int lastAttack;
    private float chargeSpeed = 2.5f;
    private void Start() {
        died = false;
        Initialize();
        slider.minValue = 0;
        slider.maxValue = health;
        attackTime = -1f;
        start = false;
        notAttacking = true;
        charging = false;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        golemBossRoom = GameObject.FindGameObjectWithTag("DungeonSceneManager").GetComponent<GolemBossRoom>();
        animator = GetComponent<Animator>();
        animator.SetInteger("Attack", 0);
        lastAttack = -1;
    }
    private void Update() {
        if (!died && health <= 0) {
            start = false;
            died = true;
            damage = 0;
            speed = 0;
            golemBossRoom.CompleteFight();
            StartCoroutine(Death());
        }

        //Starts the Boss Fight
       if (start && notAttacking) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        if (attackTime < 0 && notAttacking && start) {
            DoRandomAttack();
            notAttacking = false;
        } else {
            attackTime -= Time.deltaTime;
        }

        if (charging) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, chargeSpeed * Time.deltaTime);
        }

        //Enrage at 1/2 HP
        if (health == 30) {
            resetAttackTime = 3f;
        }
        
        animator.SetFloat("Hori", target.position.x - transform.position.x);
        animator.SetFloat("Vert", target.position.y - transform.position.y);
    }
    public void StartMoving() {
        attackTime = resetAttackTime;
        notAttacking = true;
        animator.SetTrigger("Start");
    }
    private void DoRandomAttack() {
        if (start) {
            int rand = Random.Range(1, 4);
            while (rand == lastAttack) { //Don't trigger the same attack twice
                rand = Random.Range(1, 4);
            }
            lastAttack = rand;
            if (rand == 1) {
                StartCoroutine(Charge());
            } else if (rand == 2) {
                StartCoroutine(ShootBloodBullet());
            } else {
                StartCoroutine(SpawnCandling());
            }
        }
    }
    private IEnumerator Charge() {
        //Getting ready to charge
        animator.SetInteger("Attack", 1);
        yield return new WaitForSeconds(1.8f);
        //Charge
        animator.SetTrigger("Charge");
        charging = true;
        yield return new WaitForSeconds(5f);
        charging = false;
        //Breathing in and out heavily
        animator.SetTrigger("Rest");
        yield return new WaitForSeconds(restTime);
        //End
        animator.SetInteger("Attack", 0);
        animator.SetTrigger("Walking");
        attackTime = resetAttackTime;
        notAttacking = true;
    }
    private IEnumerator ShootBloodBullet() {
        //Getting ready to shoot
        animator.SetInteger("Attack", 2);
        yield return new WaitForSeconds(1.4f);
        animator.SetTrigger("Shoot");
        //Shoot
        for (int i = 0; i < 3; i++) {
            ActuallyShoot(target.position);
            yield return new WaitForSeconds(0.7f);
        }
        animator.SetTrigger("Rest");
        yield return new WaitForSeconds(restTime);
        //End
        animator.SetInteger("Attack", 0);
        animator.SetTrigger("Walking");
        attackTime = resetAttackTime;
        notAttacking = true;
    }
    private void ActuallyShoot(Vector2 aim) {
        GameObject projectile = Instantiate(bloodBullet, transform.position, Quaternion.identity) as GameObject;
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(aim.x - transform.position.x, aim.y - transform.position.y).normalized * 10;
    }
    private IEnumerator SpawnCandling() {
        //Getting ready to spawn
        animator.SetInteger("Attack", 3);
        yield return new WaitForSeconds(2.7f);
        //Spawn
        Instantiate(candling, new Vector2(transform.position.x + Random.Range(-1f, 1f), transform.position.y + Random.Range(-1f, 1f)), Quaternion.identity);
        animator.SetTrigger("Rest");
        yield return new WaitForSeconds(restTime);
        //End
        animator.SetInteger("Attack", 0);
        animator.SetTrigger("Walking");
        attackTime = resetAttackTime;
        notAttacking = true;

        yield return null;
    }
    private IEnumerator Death() {
        StopAllCoroutines();
        speed = 0f;
        chargeSpeed = 0f;
        if ((int) DataStorage.saveValues["waxDungeonGolem"] == 1) {
            DataStorage.saveValues["waxDungeonGolem"] = 2;
        }
        animator.SetTrigger("Death");
        //Wait for the animation to finish
        yield return new WaitForSeconds(1.75f);
        Instantiate(puddle, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0f);
        Destroy(gameObject);
    }
}