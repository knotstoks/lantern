using System.Collections;
using UnityEngine;

public class CandlingHive : Enemy {
    [SerializeField] private GameObject child;
    [SerializeField] private float respawnTime;
    [SerializeField] private Animator animator;
    private float spawnTime;
    private Rigidbody2D rb;
    private bool died;
    private void Start() {
        GetSprite();
        spawnTime = respawnTime;
        rb = gameObject.GetComponent<Rigidbody2D>();
        died = false;
    }
    void Update() {
        if (health <= 0 && !died) {
            died = true;
            damage = 0;
            StartCoroutine(Death());
        }

        if (spawnTime > 0) {
            spawnTime -= Time.deltaTime;
        } else {
            Instantiate(child, new Vector2(rb.position.x + Random.Range(-2f, 2f), rb.position.y + Random.Range(-2f, 2f)), transform.rotation);
            GameObject.FindGameObjectWithTag("DungeonSceneManager").GetComponent<Manager1>().EnemiesNow(1);
            spawnTime = respawnTime;
        }
    }
    private IEnumerator Death() {
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(0.6f);
        GameObject.FindGameObjectWithTag("DungeonSceneManager").GetComponent<Manager1>().EnemiesNow(-1);
        Destroy(gameObject);
    }
}