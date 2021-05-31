using System.Collections;
using UnityEngine;
/**
Animation References:
Trigger: Melee - Tries to attack the player with a melee attack
Trigger: Idle - Goes to Idle Mode
Trigger: Death - Destroys Arm

Int: State - 1 for untouched, 2 for some cracked, 3 for very cracked
**/

public abstract class Arm : MonoBehaviour { //Tag as "Enemy"
    [SerializeField] protected int health;
    [SerializeField] protected int damage;
    [SerializeField] protected float resetMeleeAttackTime;
    [SerializeField] protected int element; //0 for fire, 1 for water, 2 for air, 3 for earth
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected float attackTime;
    protected FourArms fourArms;
    public bool start;
    public bool dead;
    public bool invulnerable;
    private void Start() {
        start = false;
        invulnerable = false;
        dead = false;
        fourArms = GameObject.FindGameObjectWithTag("Boss").GetComponent<FourArms>();
        animator = GetComponent<Animator>();
        animator.SetInteger("State", 1);
    }
    private void Update() {
        if (health <= 0 && !dead) {
            dead = true;
            Death();
        }

        if (health == 20) {
            animator.SetInteger("State", 2);
        }

        if (health == 10) {
            animator.SetInteger("State", 3);
        }

        if (attackTime <= 0) {
            if (start && !dead) { //Melee if player comes too close
                //TODO: Melee Attack
                StartCoroutine(DoMeleeAttack());
                attackTime = resetMeleeAttackTime;
            }

            if (start && dead) { //Do Special Attack
                StartCoroutine(SpecialAttack());
                attackTime = Random.Range(5f, 10f);
            }
        } else {
            attackTime -= Time.deltaTime;
        }
    }
    public abstract IEnumerator SpecialAttack();
    public IEnumerator DoMeleeAttack() {
        yield return null;
    }
    protected void Death() {
        fourArms.UnlockArms();
        animator.SetTrigger("Death");
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Bullet" && !invulnerable) {
            Rigidbody2D bullet = other.GetComponent<Rigidbody2D>();
            StartCoroutine(FlashRed());
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Player>().Damage(damage);
        }
    }
    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Player>().Damage(damage);
        }
    }
    private IEnumerator FlashRed() {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }
    public void Damage(int n) {
        if (!fourArms.targeting) {
            fourArms.LockArms(element);
        }

        if (!invulnerable) {
            health -= n;
            fourArms.Damage(n);
        }
    }
}