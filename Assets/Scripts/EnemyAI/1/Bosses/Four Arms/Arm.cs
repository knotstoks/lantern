using System.Collections;
using UnityEngine;
/**
Animation References:
Trigger: Melee - Does Melee Attack
Trigger: Death - Destroys Arm
Trigger: Glow - Sets Arms to glowing

Int: State - 1 for untouched, 2 for some cracked, 3 for very cracked
**/

public abstract class Arm : MonoBehaviour { //Tag as "Arm"
    [SerializeField] protected int health;
    [SerializeField] protected int damage;
    [SerializeField] protected int element; //0 for fire, 1 for water, 2 for air, 3 for earth
    [SerializeField] protected GameObject meleeArea;
    [SerializeField] protected float meleeResetTime;
    [SerializeField] protected GameObject fourArmsObject;
    [SerializeField] protected AudioClip hitSound;
    protected AudioSource hitAudioSource;
    protected float meleeTime;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected float attackTime;
    protected Player player;
    protected FourArms fourArms;
    public bool start;
    public bool dead;
    public bool invulnerable;
    private void Start() {
        start = false;
        invulnerable = false;
        dead = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        fourArms = fourArmsObject.GetComponent<FourArms>();
        animator = GetComponent<Animator>();
        animator.SetInteger("State", 1);
        spriteRenderer = GetComponent<SpriteRenderer>();
        meleeTime = meleeResetTime;
        attackTime = 5f;
        hitAudioSource = GetComponent<AudioSource>();
    }
    protected void Update() {
        if (health <= 0 && !dead) {
            dead = true;
            StartCoroutine(Death());
            Destroy(gameObject.GetComponent<PolygonCollider2D>());
        }

        if (health == 20) {
            animator.SetInteger("State", 2);
        }

        if (health == 10) {
            animator.SetInteger("State", 3);
        }

        if (dead) {
            if (attackTime <= 0) {
                SpecialAttack();
                attackTime = Random.Range(8f, 15f);
            } else {
                attackTime -= Time.deltaTime;
            }
        }

        if (!dead) {
            if (meleeTime <= 0) {
                meleeTime = meleeResetTime;
                StartCoroutine(MeleeAttack());
            } else {
                meleeTime -= Time.deltaTime;
            }
        }
    }
    public abstract void SpecialAttack();
    protected IEnumerator MeleeAttack() {
        animator.SetTrigger("Melee");
        //Wait for animation to finish
        yield return new WaitForSeconds(1.72f);
        if (meleeArea.GetComponent<MeleeArea>().playerIn) {
            player.Damage(1);
        }
        yield return new WaitForSeconds(1.09f);
        animator.SetTrigger("FinishedMelee");
    }
    protected IEnumerator Death() {
        fourArms.UnlockArms();
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(1.22f);
        animator.SetTrigger("Glow");
    }
    protected void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Bullet" && !invulnerable) {
            StartCoroutine(FlashRed());
        }
    }
    protected IEnumerator FlashRed() {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }
    public void Damage(int n) {
        if (!fourArms.targeting) {
            fourArms.targeting = true;
            fourArms.LockArms(element);
        }

        if (!invulnerable) {
            health -= n;
            fourArms.Damage(n);
            hitAudioSource.PlayOneShot(hitSound);
        }
    }
}