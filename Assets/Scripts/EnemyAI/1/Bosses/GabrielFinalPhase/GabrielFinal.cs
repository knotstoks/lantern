using System.Collections;
using UnityEngine;

/*
Animation References
Trigger: Idle - Flaps his wings at the top of the scene
Trigger: Imprisoned - Imprisoned in the middle of the map
Trigger: Death - Gabriel Dies
*/
public class GabrielFinal : MonoBehaviour {
    [SerializeField] private GameObject feather;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private Dialogue orbDialogue;
    public int health; //20
    private bool dead;
    private bool patternEnded;
    public bool canDamage;
    private SpriteRenderer spriteRenderer;
    private GabrielFinalRoom sceneManager;
    private Animator animator;
    private AudioSource audioSource;
    private int line;
    private DialogueManager dialogueManager;
    private bool inIntro;
    private IEnumerator Start() {
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<GabrielFinalRoom>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        canDamage = false;
        yield return new WaitForSeconds(1f);
        audioSource = GetComponent<AudioSource>();
        line = 0;
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        inIntro = false;
        StartOrbDialogue();
    }
    private void Update() {
        if (health <= 0 && !dead) {
            StopAllCoroutines();
            dead = true;
            Return();
            sceneManager.FinishFight();
            //Animation for Gabriel Second form dying
            animator.SetTrigger("Death");
        }

        if (inIntro && Input.GetKeyDown(KeyCode.E)) {
            if (line == orbDialogue.sentences.Length - 1) {
                inIntro = false;
                dialogueManager.DisplayNextSentence();
                line = 0;
                Begin();
            } else {
                dialogueManager.DisplayNextSentence();
                line++;
            }
        }
    }
    private void Begin() {
        StartFeatherPattern();
    }
    private void StartOrbDialogue() {
        dialogueManager.StartDialogue(orbDialogue);
        inIntro = true;
    }
    public void Imprison() {
        canDamage = true;
        //Teleport Gabriel to center and imprison
        transform.position = new Vector2(0.57f, 8f);
        animator.SetTrigger("Imprisoned");
    }
    public void Return() {
        if (!dead) {
            canDamage = false;
            //Teleport Gabriel back
            transform.position = new Vector2(0.57f, 9.08f);
            animator.SetTrigger("Idle");
        }
    }
    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Player>().Damage(1);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (canDamage && other.tag == "Bullet") {
            StartCoroutine(FlashRed());
            Damage(1);
        }
    }
    private IEnumerator FlashRed() {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }
    public void Damage(int n) {
        health -= n;
        StartCoroutine(FlashRed());
        audioSource.PlayOneShot(audioClip);
    }
    //Code for all the Feather Patterns
    private void StartFeatherPattern() {
        StartCoroutine(Pattern1());
    }
    public void Activate(Vector2 spawnWhere, Vector2 shootWhere) {
        GameObject projectile = Instantiate(feather, spawnWhere, Quaternion.identity);
        projectile.GetComponent<Feather>().endPosition = shootWhere;
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(shootWhere.x - spawnWhere.x, shootWhere.y - spawnWhere.y).normalized * 3;
    }
    private IEnumerator Pattern1() {
        yield return new WaitForSeconds(1f);
        StartCoroutine(VShape()); //PUT COROUTINES HERE!
        yield return new WaitForSeconds(2f);
        StartCoroutine(OrbAttackLR());
        yield return new WaitForSeconds(2f);
        OrbAttackUD();
        yield return new WaitForSeconds(1f);
        StartCoroutine(DiagonalPathway1());
        yield return new WaitForSeconds(7f);
        RightToLeft3();
        LeftToRight3();
        yield return new WaitForSeconds(1f);
        StartCoroutine(VShapeFlipped());
        yield return new WaitForSeconds(4f);
        CircularShotsDown();
        yield return new WaitForSeconds(4f);
        OrbAttackUD();
        yield return new WaitForSeconds(4f);
        BottomFromLtoR();
        yield return new WaitForSeconds(3f);
        if (!dead) {
            StartCoroutine(Pattern2());
        }
    }
    private IEnumerator Pattern2() {
        yield return new WaitForSeconds(3f);
        DiagonalDownL();
        yield return new WaitForSeconds(3f);
        OrbAttackLR();
        yield return new WaitForSeconds(1f);
        OrbAttackUD();
        CircularShotsUp();
        yield return new WaitForSeconds(2f);
        RightToLeft3();
        LeftToRight3();
        yield return new WaitForSeconds(3f);
        DiagonalDownR();
        yield return new WaitForSeconds(3f);
        if (!dead) {
            StartCoroutine(Pattern1());
        }
    }
    private IEnumerator VShape() {
        // arrows downwards in a V shape
        Activate(new Vector2(0, 8), new Vector2(0, -1));
        yield return new WaitForSeconds(1f);
        Activate(new Vector2(-2, 8), new Vector2(-2, -1));
        Activate(new Vector2(2, 8), new Vector2(2, -1));
        yield return new WaitForSeconds(1f);
        Activate(new Vector2(-4, 8), new Vector2(-4, -1));
        Activate(new Vector2(4, 8), new Vector2(4, -1));
        Activate(new Vector2(-8, 0), new Vector2(1, 0));
        Activate(new Vector2(8, 0), new Vector2(-1, 0));
    }
    private IEnumerator VShapeFlipped() {
        Activate(new Vector2(0, -8), new Vector2(0, 1));
        yield return new WaitForSeconds(1f);
        Activate(new Vector2(-2, -8), new Vector2(-2, 1));
        Activate(new Vector2(2, -8), new Vector2(2, 1));
        yield return new WaitForSeconds(1f);
        Activate(new Vector2(-4, -8), new Vector2(-4, 1));
        Activate(new Vector2(4, -8), new Vector2(4, 1));
        Activate(new Vector2(-8, 0), new Vector2(1, 0));
        Activate(new Vector2(8, 0), new Vector2(-1, 0));
    }
    private IEnumerator OrbAttackLR() {
        // top and bottom row arrows LR
        Activate(new Vector2(-8, 6), new Vector2(1, 6));
        Activate(new Vector2(8, 6), new Vector2(-1, 6));
        Activate(new Vector2(-8, -6), new Vector2(1, -6));
        Activate(new Vector2(8, -6), new Vector2(-1, -6));
        yield return new WaitForSeconds(1f);
        Activate(new Vector2(-8, 4), new Vector2(1, 4));
        Activate(new Vector2(8, 4), new Vector2(-1, 4));
        Activate(new Vector2(-8, -4), new Vector2(1, -4));
        Activate(new Vector2(8, -4), new Vector2(-1, -4));
    }
    private void OrbAttackUD() {
        // attack at player standing at orb UD
        Activate(new Vector2(-6, 4), new Vector2(-6, -1));
        Activate(new Vector2(6, 4), new Vector2(6, -1));
        Activate(new Vector2(-6, 4), new Vector2(-6, 1));
        Activate(new Vector2(6, 4), new Vector2(6, 1));
        Activate(new Vector2(0, 8), new Vector2(0, -1));
    }
    private IEnumerator DiagonalPathway1() {
        // diagonal pathway v v v ^ ^ ^
        Activate(new Vector2(-7, 8), new Vector2(-7, -1));
        Activate(new Vector2(-6, 9), new Vector2(-6, -1));
        Activate(new Vector2(-5, 10), new Vector2(-5, -1));
        // Activate(new Vector2(-4, 11), new Vector2(-4, -1));
        Activate(new Vector2(-3, 12), new Vector2(-3, -1));
        Activate(new Vector2(-2, 13), new Vector2(-2, -1));
        Activate(new Vector2(-1, 14), new Vector2(-1, -1));
        Activate(new Vector2(0, 15), new Vector2(0, -1));
        Activate(new Vector2(1, 16), new Vector2(1, -1));
        Activate(new Vector2(2, 17), new Vector2(2, -1));
        Activate(new Vector2(3, 18), new Vector2(3, -1));
        Activate(new Vector2(4, 19), new Vector2(4, -1));
        Activate(new Vector2(5, 20), new Vector2(5, -1));
        Activate(new Vector2(6, 21), new Vector2(6, -1));
        Activate(new Vector2(7, 22), new Vector2(7, -1));
        yield return new WaitForSeconds(2f);
        Activate(new Vector2(-7, -22), new Vector2(-7, 1));
        Activate(new Vector2(-6, -21), new Vector2(-6, 1));
        Activate(new Vector2(-5, -20), new Vector2(-5, 1));
        Activate(new Vector2(-4, -19), new Vector2(-4, 1));
        Activate(new Vector2(-3, -18), new Vector2(-3, 1));
        Activate(new Vector2(-2, -17), new Vector2(-2, 1));
        Activate(new Vector2(-1, -16), new Vector2(-1, 1));
        Activate(new Vector2(0, -15), new Vector2(0, 1));
        Activate(new Vector2(1, -14), new Vector2(1, 1));
        Activate(new Vector2(2, -13), new Vector2(2, 1));
        Activate(new Vector2(3, -12), new Vector2(3, 1));
        // Activate(new Vector2(4, -11), new Vector2(4, 1));
        Activate(new Vector2(5, -10), new Vector2(5, 1));
        Activate(new Vector2(6, -9), new Vector2(6, 1));
        Activate(new Vector2(7, -8), new Vector2(7, 1));
    }
    private IEnumerator DiagonalPathway2() {
        // diagonal pathway v v v ^ ^ ^
        Activate(new Vector2(-7, 8), new Vector2(-7, -1));
        Activate(new Vector2(-6, 9), new Vector2(-6, -1));
        Activate(new Vector2(-5, 10), new Vector2(-5, -1));
        Activate(new Vector2(-4, 11), new Vector2(-4, -1));
        Activate(new Vector2(-3, 12), new Vector2(-3, -1));
        Activate(new Vector2(-2, 13), new Vector2(-2, -1));
        Activate(new Vector2(-1, 14), new Vector2(-1, -1));
        // Activate(new Vector2(0, 15), new Vector2(0, -1));
        Activate(new Vector2(1, 16), new Vector2(1, -1));
        Activate(new Vector2(2, 17), new Vector2(2, -1));
        Activate(new Vector2(3, 18), new Vector2(3, -1));
        Activate(new Vector2(4, 19), new Vector2(4, -1));
        Activate(new Vector2(5, 20), new Vector2(5, -1));
        Activate(new Vector2(6, 21), new Vector2(6, -1));
        Activate(new Vector2(7, 22), new Vector2(7, -1));
        yield return new WaitForSeconds(2f);
        Activate(new Vector2(-7, -22), new Vector2(-7, 1));
        Activate(new Vector2(-6, -21), new Vector2(-6, 1));
        Activate(new Vector2(-5, -20), new Vector2(-5, 1));
        Activate(new Vector2(-4, -19), new Vector2(-4, 1));
        Activate(new Vector2(-3, -18), new Vector2(-3, 1));
        Activate(new Vector2(-2, -17), new Vector2(-2, 1));
        Activate(new Vector2(-1, -16), new Vector2(-1, 1));
        // Activate(new Vector2(0, -15), new Vector2(0, 1));
        Activate(new Vector2(1, -14), new Vector2(1, 1));
        Activate(new Vector2(2, -13), new Vector2(2, 1));
        Activate(new Vector2(3, -12), new Vector2(3, 1));
        Activate(new Vector2(4, -11), new Vector2(4, 1));
        Activate(new Vector2(5, -10), new Vector2(5, 1));
        Activate(new Vector2(6, -9), new Vector2(6, 1));
        Activate(new Vector2(7, -8), new Vector2(7, 1));
    }
    private void RightToLeft3() {
        // spawn < < <
        Activate(new Vector2(10, 3), new Vector2(-1, 3));
        Activate(new Vector2(9, 2), new Vector2(-1, 2));
        Activate(new Vector2(8, 1), new Vector2(-1, 1));
        Activate(new Vector2(8, -1), new Vector2(-1, -1));
        Activate(new Vector2(9, -2), new Vector2(-1, -2));
        Activate(new Vector2(10, -3), new Vector2(-1, -3));
    }
    private void LeftToRight3() {
        // spawn > > >
        Activate(new Vector2(-10, 3), new Vector2(-1, 3));
        Activate(new Vector2(-9, 2), new Vector2(-1, 2));
        Activate(new Vector2(-8, 1), new Vector2(-1, 1));
        Activate(new Vector2(-8, -1), new Vector2(-1, -1));
        Activate(new Vector2(-9, -2), new Vector2(-1, -2));
        Activate(new Vector2(-10, -3), new Vector2(-1, -3));
    }
    private void CircularShotsDown() {
        // spawn circular shots downwards
        Activate(new Vector2(-8, 8), new Vector2(0, 0));
        Activate(new Vector2(-4, 10), new Vector2(0, 0));
        Activate(new Vector2(0, 12), new Vector2(0, 0));
        Activate(new Vector2(4, 10), new Vector2(0, 0));
        Activate(new Vector2(8, 8), new Vector2(0, 0));
    }
    private void CircularShotsUp() {
        //spawn circular shots upwards
        Activate(new Vector2(-8, -8), new Vector2(0, 0));
        Activate(new Vector2(-4, -10), new Vector2(0, 0));
        Activate(new Vector2(0, -12), new Vector2(0, 0));
        Activate(new Vector2(4, -10), new Vector2(0, 0));
        Activate(new Vector2(8, -8), new Vector2(0, 0));
    }
    private void BottomFromLtoR() {
        Activate(new Vector2(-10, -6), new Vector2(1, -6));
        Activate(new Vector2(-9, -5), new Vector2(1, -5));
        Activate(new Vector2(-8, -4), new Vector2(1, -4));
    }
    private void BottomFromRtoL() {
        Activate(new Vector2(10, -6), new Vector2(-1, -6));
        Activate(new Vector2(9, -5), new Vector2(-1, -5));
        Activate(new Vector2(8, -4), new Vector2(-1, -4));
    }
    private void DiagonalDownL() {
        Activate(new Vector2(-8, 20), new Vector2(20, -8));
        Activate(new Vector2(-8, 18), new Vector2(18, -8));
        Activate(new Vector2(-8, 16), new Vector2(16, -8));
        Activate(new Vector2(-8, 14), new Vector2(14, -8));
        Activate(new Vector2(-8, 12), new Vector2(12, -8));
        Activate(new Vector2(-8, 10), new Vector2(10, -8));
        Activate(new Vector2(-8, 8), new Vector2(8, -8));
        // Activate(new Vector2(-8, 6), new Vector2(6, -8));
        Activate(new Vector2(-8, 4), new Vector2(4, -8));
        Activate(new Vector2(-8, 2), new Vector2(2, -8));
        Activate(new Vector2(-8, 0), new Vector2(0, -8));
        Activate(new Vector2(-8, -2), new Vector2(-2, -8));
        Activate(new Vector2(-8, -4), new Vector2(-4, -8));
        Activate(new Vector2(-8, -6), new Vector2(-6, -8));
        Activate(new Vector2(-8, -8), new Vector2(-8, -8));
    }
    private void DiagonalDownR() {
        Activate(new Vector2(8, 20), new Vector2(20, 8));
        Activate(new Vector2(8, 18), new Vector2(18, 8));
        Activate(new Vector2(8, 16), new Vector2(16, 8));
        Activate(new Vector2(8, 14), new Vector2(14, 8));
        Activate(new Vector2(8, 12), new Vector2(12, 8));
        Activate(new Vector2(8, 10), new Vector2(10, 8));
        Activate(new Vector2(8, 8), new Vector2(8, 8));
        // Activate(new Vector2(8, 6), new Vector2(6, 8));
        Activate(new Vector2(8, 4), new Vector2(4, 8));
        Activate(new Vector2(8, 2), new Vector2(2, 8));
        Activate(new Vector2(8, 0), new Vector2(0, 8));
        Activate(new Vector2(8, -2), new Vector2(-2, 8));
        Activate(new Vector2(8, -4), new Vector2(-4, 8));
        Activate(new Vector2(8, -6), new Vector2(-6, 8));
        Activate(new Vector2(8, -8), new Vector2(-8, 8));
    }
}