using System.Collections;
using UnityEngine;

public class WaxSlime : Enemy {
    [SerializeField] private Animator animator;
    [SerializeField] private float resetTime; //time the slime pauses at each point
    private float minX, minY, maxX, maxY;
    private float waitTime; //time variable
    private Vector2 pos;
    private Manager1 dungeonManager;
    private bool died;
    private void Start() {
        GetSprite();
        waitTime = 0.2f;
        dungeonManager = GameObject.FindGameObjectWithTag("DungeonSceneManager").GetComponent<Manager1>();
        float x = transform.position.x;
        float y = transform.position.y;
        for (int i = 0; i < dungeonManager.spawnAreas.Length; i++) {
            if (x >= dungeonManager.spawnAreas[i].minX && x < dungeonManager.spawnAreas[i].maxX 
            && y >= dungeonManager.spawnAreas[i].minY && y < dungeonManager.spawnAreas[i].maxY) {
                minX = dungeonManager.spawnAreas[i].minX;
                maxX = dungeonManager.spawnAreas[i].maxX;
                minY = dungeonManager.spawnAreas[i].minY;
                maxY = dungeonManager.spawnAreas[i].maxY;
                break;
            }
        }
        Vector2 pos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        died = false;
    }

    private void Update() {
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