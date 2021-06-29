using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherDestroyer : MonoBehaviour {
    public void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Feather") {
            Destroy(other.gameObject);
        }
    }
}
