using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleChest : Enemy { 
    private Transform target;
    private bool ready;

    private void Start() {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update() {
        if (health <= 0) {
            Destroy(gameObject);
        }

        if (Vector2.Distance(transform.position, target.position) > 0) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        if (ready && Vector2.Distance(transform.position, target.position) < 2) {
            //Flame Belly
        }
    }
}
