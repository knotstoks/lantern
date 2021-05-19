using System.Collections;
using UnityEngine;

public class Tear : MonoBehaviour {
    [SerializeField] private float lifeTime;
    private Rigidbody2D rb;
    private void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(DeathDelay());
    }
    private IEnumerator DeathDelay() {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
    public void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") { //Damages Player
            other.GetComponent<Enemy>().Damage(1);
            Destroy(gameObject);
        }

        if (other.tag == "Invincible") { //Gets Destroyed when it hits something Invincible (eg. shields or walls)
            Destroy(gameObject);
        }
    }
}