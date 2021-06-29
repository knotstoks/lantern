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
    private bool damaging;
    private Player player;
    private void Start() {
        damaging = false;
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
        damaging = true;
        yield return new WaitForSeconds(onTime);
        damaging = false;
        animator.SetTrigger("WindDown");
        yield return new WaitForSeconds(0.6f);
        animator.SetInteger("Direction", -1);
        animator.SetTrigger("Reset");
        done = false;
        countdownTime = resetCountdownTime;
    }
    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Player" && damaging) {
            player.Damage(1);
        }
    }
}