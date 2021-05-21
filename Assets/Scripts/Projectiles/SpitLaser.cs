using System.Collections;
using UnityEngine;

public class SpitLaser : MonoBehaviour {
    [SerializeField] private int direction;
    [SerializeField] private float resetCountdownTime;
    [SerializeField] private float onTime;
    [SerializeField] private Animator animator;
    private float countdownTime;
    private bool done;
    private void Start() {
        countdownTime = resetCountdownTime;
        done = false;
        animator.SetInteger("Direction", -1);
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
        animator.SetTrigger("WindDown");
        yield return new WaitForSeconds(0.6f);
        animator.SetInteger("Direction", -1);
        animator.SetTrigger("Reset");
        done = false;
        countdownTime = resetCountdownTime;
    }
}