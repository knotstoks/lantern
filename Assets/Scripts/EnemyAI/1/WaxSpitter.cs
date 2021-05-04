﻿using UnityEngine;

public class WaxSpitter : Enemy {
    [SerializeField] private float resetSpitTime;
    private Transform target;
    private float spitTime;


    void Start() {
        spitTime = resetSpitTime;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update() {
        if (spitTime > 0) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            spitTime -= Time.deltaTime;
        } else {
            //Trigger the spit

        }
    }

    //Make a coroutine for the spit
}
