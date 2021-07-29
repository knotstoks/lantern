using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthArea : MonoBehaviour {
    public bool playerIn;
    private void Start() {
        playerIn = false;
    }
    private void OnTriggerEnter2D(Collider2D other) {
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
