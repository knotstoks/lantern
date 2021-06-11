using System.Collections;
using UnityEngine;

public class FireCircleFourArms : MonoBehaviour {
    private bool playerIn;
    private Player player;

    private IEnumerator Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        StartCoroutine(Attack());
        yield return new WaitForSeconds(1.07f);
        Destroy(gameObject);
    }
    private IEnumerator Attack() {
        yield return new WaitForSeconds(0.666f);
        if (playerIn) {
            player.Damage(1);
        }
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