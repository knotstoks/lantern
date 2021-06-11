using System.Collections;
using UnityEngine;

public class PuddleAttackFourArms : MonoBehaviour {
    private Animator animator;
    private bool playerIn;
    private Player player;

    private void Start() {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    public IEnumerator Attack() {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(2.166f);
        if (playerIn) {
            player.Damage(1);
        }
        yield return new WaitForSeconds(2.166f);
        animator.SetTrigger("Idle");
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
