using System.Collections;
using UnityEngine;

//TODO: Sprite, Animation
public class MotherWaxSlime : Enemy {
    [SerializeField] private float minX;
    [SerializeField] private float minY;
    [SerializeField] private float maxX;
    [SerializeField] private float maxY;
    [SerializeField] private float resetTime; //time the slime pauses at each point
    [SerializeField] private GameObject childSlime;
    [SerializeField] private Animator animator;
    private float waitTime; //time variable
    private Vector2 pos;
    void Start() {
        waitTime = resetTime;
        Vector2 pos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    void Update() {
        transform.position = Vector2.MoveTowards(transform.position, pos, speed * Time.deltaTime);
        waitTime -= Time.deltaTime;

        if (waitTime <= 0) {
            waitTime = resetTime;
            pos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        }

        if (health <= 0) {
            GameObject.FindGameObjectWithTag("DungeonSceneManager").GetComponent<Manager1>().EnemiesNow(1);
            Instantiate(childSlime, transform.position + new Vector3(0.5f, 0, 0), transform.rotation);
            Instantiate(childSlime, transform.position - new Vector3(0.5f, 0, 0), transform.rotation);
            Destroy(gameObject);
        }

        if (((Vector2) transform.position - pos).magnitude < 0.01) { 
            animator.SetFloat("Hori", 0);
            animator.SetFloat("Vert", -1);
        } else {
            animator.SetFloat("Hori", pos.x - transform.position.x);
            animator.SetFloat("Vert", pos.y - transform.position.y);
        }
    }
}