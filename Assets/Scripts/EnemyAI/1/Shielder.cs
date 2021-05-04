using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shielder : Enemy { //Shielder that can only be damaged from the back
    private Transform target;
    private Rigidbody2D rb;
    private float distX;
    private float distY;

    private void Start() {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (health <= 0) {
            Destroy(gameObject);
        }

        distX = target.position.x - rb.position.x;
        distY = target.position.y - rb.position.y;

        if (Mathf.Abs(distX) > Mathf.Abs(distY)) {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, rb.position.y), speed * Time.deltaTime);
        } else {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(rb.position.x, target.position.y), speed * Time.deltaTime);   
        }
    }
}