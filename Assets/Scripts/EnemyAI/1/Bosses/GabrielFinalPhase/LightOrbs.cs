using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOrbs : MonoBehaviour {
    public int charge;
    private void Start() {
        charge = 0;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        
    }
}
