﻿using System.Collections;
using UnityEngine;

public class GolemCandling : Enemy {
    [SerializeField] private Animator animator;
    private Transform target;
    private Rigidbody2D rb;
    private Vector2 move;
    private bool died;
    private void Start() {
        GetSprite();
        rb = gameObject.GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        died = false;
    }

    private void Update() {
        if (health <= 0 && !died) {
            died = true;
            damage = 0;
            StartCoroutine(Death());
        }
    }
    private void FixedUpdate() {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        animator.SetFloat("Hori", target.position.x - transform.position.x);
        animator.SetFloat("Vert", target.position.y - transform.position.y);
    }
    private IEnumerator Death() {
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
    }
}