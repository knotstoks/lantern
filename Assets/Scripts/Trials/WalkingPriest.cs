using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingPriest : MonoBehaviour {
    [SerializeField] private Vector2[] positions;
    [SerializeField] private float[] times;
    private int walk;
    private bool walking;
    private Animator animator;
    private Player player;
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        animator = GetComponent<Animator>();
        walking = false;
        walk = 0;
        StartCoroutine(Walking());
    }
    private IEnumerator Walking() {
        walking = true;
        yield return new WaitForSeconds(times[walk]);
        walking = false;

        if (walk < positions.Length - 1) {
            walk++;
            StartCoroutine(Walking());
        } else {
            Destroy(gameObject);
        }
    }
    private void Update() {
        if (walking) {
            transform.position = Vector2.MoveTowards(transform.position, positions[walk], 3 * Time.deltaTime);
        } else {
            transform.position = transform.position;
        }

        animator.SetFloat("posX", positions[walk].x - transform.position.x);
        animator.SetFloat("posY", positions[walk].y - transform.position.y);
    }
}
