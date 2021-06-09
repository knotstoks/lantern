using System.Collections;
using UnityEngine;

public class MeleeArea : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    public bool playerIn;
    private void Start() {
        playerIn = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
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
