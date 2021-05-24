using System.Collections;
using UnityEngine;

public class WaxSlime : Enemy {
    [SerializeField] private Animator animator;
    private float minX, minY, maxX, maxY;
    [SerializeField] private float resetTime; //time the slime pauses at each point
    private float waitTime; //time variable
    private Vector2 pos;
    private Manager1 dungeonManager;
    private bool died;
    void Start() {
        GetSprite();
        waitTime = resetTime;
        dungeonManager = GameObject.FindGameObjectWithTag("DungeonSceneManager").GetComponent<Manager1>();
        minX = dungeonManager.allMinX;
        minY = dungeonManager.allMinY;
        maxX = dungeonManager.allMaxX;
        maxY = dungeonManager.allMaxY;
        Vector2 pos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        died = false;
    }

    void Update() {
        transform.position = Vector2.MoveTowards(transform.position, pos, speed * Time.deltaTime);
        waitTime -= Time.deltaTime;

        if (waitTime <= 0) {
            waitTime = resetTime;
            pos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        }

        if (health <= 0 && !died) {
            died = true;
            damage = 0;
            StartCoroutine(Death());
        }

        if (((Vector2) transform.position - pos).magnitude < 0.01) { 
            animator.SetFloat("Hori", 0);
            animator.SetFloat("Vert", -1);
        } else {
            animator.SetFloat("Hori", pos.x - transform.position.x);
            animator.SetFloat("Vert", pos.y - transform.position.y);
        }
    }
    private IEnumerator Death() {
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(0.6f);
        GameObject.FindGameObjectWithTag("DungeonSceneManager").GetComponent<Manager1>().EnemiesNow(-1);
        Destroy(gameObject);
    }
}