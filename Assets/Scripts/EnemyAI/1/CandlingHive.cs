using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandlingHive : Enemy {
    [SerializeField] private GameObject child;
    [SerializeField] private float respawnTime;
    private float spawnTime;
    private Rigidbody2D rb;
    void Start() {
        GetSprite();
        spawnTime = respawnTime;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (health <= 0) {
            GameObject.FindGameObjectWithTag("DungeonSceneManager").GetComponent<Manager1>().EnemiesNow(-1);
            Destroy(gameObject);
        }

        if (spawnTime > 0) {
            spawnTime -= Time.deltaTime;
        } else {
            Instantiate(child, new Vector3(rb.position.x + Random.Range(-2f, 2f), rb.position.y + Random.Range(-2f, 2f), 0), transform.rotation);
            GameObject.FindGameObjectWithTag("DungeonSceneManager").GetComponent<Manager1>().EnemiesNow(1);
            spawnTime = respawnTime;
        }
    }
}