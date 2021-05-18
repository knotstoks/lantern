using System.Collections;
using UnityEngine;

//Shoots a laser beam, mounted to a wall
public class WaxSpitter : Enemy {
    [SerializeField] private float resetSpitTime;
    [SerializeField] private Animator animator;
    private int facingDirection; //0 - 3, NESW
    private float spitTime;


    void Start() {
        spitTime = resetSpitTime;
        facingDirection = Random.Range(0, 3);
        animator.SetFloat("facingDirection", facingDirection);
    }

    void Update() {
        if (health <= 0) {
            GameObject.FindGameObjectWithTag("DungeonSceneManager").GetComponent<Manager1>().EnemiesNow(-1);
            Destroy(gameObject);
        }

        if (spitTime > 0) {
            spitTime -= Time.deltaTime;
        } else {
            //Trigger the spit
            StartCoroutine(Spit(facingDirection));
        }
    }

    private IEnumerator Spit(int direction) {
        //TODO: Instantiate the laser

        facingDirection = Random.Range(0, 3); //Sets new direction
        yield return null;
    }
}
