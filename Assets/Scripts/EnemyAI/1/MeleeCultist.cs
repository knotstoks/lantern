using UnityEngine;

//TODO: Sprite, Animation THIS IS XIN YAN'S PROBLEM CHILD
public class MeleeCultist : Enemy {
    private Transform target;
    [SerializeField] private float attackRange;
    [SerializeField] private float restartAttack;
    private float attackTime;
    private void Start() {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        attackTime = restartAttack;
    }

    private void Update() {
        if (health <= 0) {
            Destroy(gameObject);
        }

        if (Vector2.Distance(transform.position, target.position) > 0) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        if (attackTime > 0) {
            attackTime -= Time.deltaTime;
        } else {
            if (Vector2.Distance(transform.position, target.position) < 1) {
                //TODO: The animation for slashing
                
            }
        }
    }  

    public void OnTriggerEnter2D() {
        
    }
}