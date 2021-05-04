using UnityEngine;

//TODO: Sprite, Animation
public class WaxSlime : Enemy {
    [SerializeField] private float minX;
    [SerializeField] private float minY;
    [SerializeField] private float maxX;
    [SerializeField] private float maxY;
    [SerializeField] private float resetTime; //time the slime pauses at each point
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
            Destroy(gameObject);
        }
    }
}