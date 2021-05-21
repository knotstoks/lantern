using System.Collections;
using UnityEngine;

//Shoots a laser beam, mounted to a wall
public class WaxSpitter : MonoBehaviour {
    [SerializeField] private float resetSpitTime;
    [SerializeField] private Animator animator;
    [SerializeField] private int direction;
    private float spitTime;


    void Start() {
        spitTime = resetSpitTime;
        animator.SetInteger("Direction", direction);
    }

    void Update() {
        if (spitTime > 0) {
            spitTime -= Time.deltaTime;
        } else {
            //Trigger the spit
            animator.SetTrigger("Spit");
        }
    }
}