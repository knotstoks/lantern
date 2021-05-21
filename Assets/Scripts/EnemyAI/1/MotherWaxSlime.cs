using System.Collections;
using UnityEngine;

//TODO: Sprite, Animation
public class MotherWaxSlime : Enemy {
    [SerializeField] private Animator animator;
    private float minX, minY, maxX, maxY;
    [SerializeField] private float resetTime; //time the slime pauses at each point
    [SerializeField] private GameObject childSlime;
    private float waitTime; //time variable
    private Vector2 pos;
    private Manager1 dungeonManager;
    private bool spawned;
    private bool died;
    void Start() {
        GetSprite();
        waitTime = resetTime;
        dungeonManager = GameObject.FindGameObjectWithTag("DungeonSceneManager").GetComponent<Manager1>();
        minX = dungeonManager.minX;
        minY = dungeonManager.minY;
        maxX = dungeonManager.maxX;
        maxY = dungeonManager.maxY;
        Vector2 pos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        spawned = false;
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
            spawned = true;
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
        if (!spawned) {
            GameObject.FindGameObjectWithTag("DungeonSceneManager").GetComponent<Manager1>().EnemiesNow(1);
            Instantiate(childSlime, transform.position + new Vector3(0.5f, 0, 0), transform.rotation);
            Instantiate(childSlime, transform.position - new Vector3(0.5f, 0, 0), transform.rotation);
            animator.SetTrigger("Death");
            yield return new WaitForSeconds(0.6f);
            Destroy(gameObject);
        }
    }
}