using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaxDebuffer : MonoBehaviour {
    [SerializeField] private GameObject tear;
    [SerializeField] private float resetShootTime;
    [SerializeField] private float tearSpeed;
    [SerializeField] private int direction; //0 - 3, NESW
    [SerializeField] private Animator animator;
    private float shootTime;
    private Vector2[] shootDirection = {
        new Vector2(0, 1), //Up
        new Vector2(-1, 0), //Left
        new Vector2(0, -1), //Down
        new Vector2(1, 0) //Right
    };
    private void Start() {
        animator.SetBool("Crying", false);
        shootTime = resetShootTime;
    }
    private void FixedUpdate() {
        if (shootTime > 0) {
            shootTime -= Time.deltaTime;
        } else {
            Cry();
            shootTime = resetShootTime;
        }
    }
    private void Cry() {
        animator.SetBool("Crying", true);
        GameObject projectile = Instantiate(tear, transform.position, Quaternion.identity) as GameObject;
        projectile.GetComponent<Rigidbody2D>().velocity = shootDirection[direction] * tearSpeed;
        animator.SetBool("Crying", false);
    }
}