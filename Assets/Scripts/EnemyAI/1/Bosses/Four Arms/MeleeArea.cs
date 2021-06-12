using System.Collections;
using UnityEngine;

public class MeleeArea : MonoBehaviour {
    public bool playerIn;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private void Start() {
        animator = GetComponent<Animator>();
        playerIn = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public IEnumerator Ripple() {
        animator.SetBool("Ripple", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Ripple", false);
    }
    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            playerIn = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            playerIn = false;
        }
    }
}
