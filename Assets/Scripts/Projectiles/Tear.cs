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
            other.GetComponent<Player>().SlowPlayer();
            Destroy(gameObject);
        }
    }
}