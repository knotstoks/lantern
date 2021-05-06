using UnityEngine;

//TODO: Sprite, Animation
public class RangedCultist : Enemy {
    private Transform target;
    private void Start() {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update() {
        if (health <= 0) {
            GameObject.FindGameObjectWithTag("DungeonSceneManager").GetComponent<Manager1>().EnemiesNow(-1);
            Destroy(gameObject);
        }

        if (Vector2.Distance(transform.position, target.position) > 5) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        //TODO: Shoot the Player when in the line
    }
}
