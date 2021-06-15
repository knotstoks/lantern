using System.Collections;
using UnityEngine;

public class SpitLaser : MonoBehaviour {
    [SerializeField] private int direction;
    [SerializeField] private float resetCountdownTime;
    [SerializeField] private float onTime;
    [SerializeField] private Animator animator;
    private BoxCollider2D boxCollider;
    private float countdownTime;
    private bool done;
    private bool playerIn;
    private Player player;
    private void Start() {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        countdownTime = resetCountdownTime;
        done = false;
        animator.SetInteger("Direction", -1);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void FixedUpdate() {
        if (!done) {
            if (countdownTime > 0) {
                countdownTime -= Time.deltaTime;
            } else {
                done = true;
                StartCoroutine(StartSpit());
            }
        }
    }
    private IEnumerator StartSpit() {
        animator.SetInteger("Direction", direction);
        yield return new WaitForSeconds(0.6f);
        animator.SetTrigger("Idle");
        yield return new WaitForSeconds(onTime);
        if (playerIn) {
            player.Damage(1);
        }
        animator.SetTrigger("WindDown");
        yield return new WaitForSeconds(0.6f);
        animator.SetInteger("Direction", -1);
        animator.SetTrigger("Reset");
        done = false;
        countdownTime = resetCountdownTime;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") { //Damages Player
            playerIn = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") { //Damages Player
            playerIn = false;
        }
    }
}