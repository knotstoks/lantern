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
    [SerializeField] private float resetSwitchAttackTime;
    [SerializeField] private float restTime;
    [SerializeField] private GameObject bloodBullet;
    [SerializeField] private Vector2 bulletTilt;
    public bool start;
    private Transform target;
    private GolemBossRoom golemBossRoom;
    private float switchAttackTime;
    private Animator animator;
    private bool notAttacking;
    private void Start() {
        slider.minValue = 0;
        slider.maxValue = health;
        start = false;
        notAttacking = false;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        golemBossRoom = GameObject.FindGameObjectWithTag("DungeonSceneManager").GetComponent<GolemBossRoom>();
        animator = GetComponent<Animator>();
        animator.SetInteger("Attack", 0);
    }
    private void FixedUpdate() {
        if (health <= 0) {
            damage = 0;
            golemBossRoom.CompleteFight();
            Death();
        }

        //Starts the Boss Fight
       if (start && notAttacking) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        if (switchAttackTime < 0) {
            DoRandomAttack();
            notAttacking = false;
        } else {
            switchAttackTime -= Time.deltaTime;
        }

        
        animator.SetFloat("Hori", target.position.x - transform.position.x);
        animator.SetFloat("Vert", target.position.y - transform.position.y);
    }
    public void StartMoving() {
        switchAttackTime = resetSwitchAttackTime;
        animator.SetTrigger("Start");
    }
    private void DoRandomAttack() {
        int rand = Random.Range(1, 4);
        if (rand == 1) {
            StartCoroutine(Charge());
        } else if (rand == 2) {
            StartCoroutine(ShootBloodBullet());
        } else {
            StartCoroutine(SpawnCandling());
        }
    }
    private IEnumerator Charge() {
        //Getting ready to charge

        //Charge

        //Breathing in and out heavily
        animator.SetTrigger("Rest");
        yield return new WaitForSeconds(restTime);
        //End
        animator.SetInteger("Attack", 0);
        animator.SetTrigger("Walking");
        switchAttackTime = resetSwitchAttackTime;
        notAttacking = true;
    }
    private IEnumerator ShootBloodBullet() {
        //Getting ready to shoot

        //Shoot
        GameObject projectile1 = Instantiate(bloodBullet, transform.position, Quaternion.identity) as GameObject;
        GameObject projectile2 = Instantiate(bloodBullet, transform.position, Quaternion.identity) as GameObject;
        GameObject projectile3 = Instantiate(bloodBullet, transform.position, Quaternion.identity) as GameObject;
        Vector2 v1 = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
        Vector2 v2 = new Vector2(target.position.x - transform.position.x + bulletTilt.x, target.position.y - transform.position.y + bulletTilt.y);
        Vector2 v3 = new Vector2(target.position.x - transform.position.x - bulletTilt.x, target.position.y - transform.position.y - bulletTilt.y);
        projectile1.GetComponent<Rigidbody2D>().velocity = v1;
        projectile2.GetComponent<Rigidbody2D>().velocity = v2;
        projectile3.GetComponent<Rigidbody2D>().velocity = v3;
        yield return new WaitForSeconds(restTime);
        //End
        animator.SetInteger("Attack", 0);
        animator.SetTrigger("Walking");
        switchAttackTime = resetSwitchAttackTime;
        notAttacking = true;
    }
    private IEnumerator SpawnCandling() {
        //Getting ready to spawn

        //Spawn

        yield return new WaitForSeconds(restTime);
        //End
        animator.SetInteger("Attack", 0);
        animator.SetTrigger("Walking");
        switchAttackTime = resetSwitchAttackTime;
        notAttacking = true;

        yield return null;
    }
    private IEnumerator Death() {
        animator.SetTrigger("Death");
        //Wait for the animation to finish
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}