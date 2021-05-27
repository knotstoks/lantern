using System.Collections;
using UnityEngine;

public class BloodBullet : MonoBehaviour {
    [SerializeField] private float lifeTime;
    private void Start() {
        StartCoroutine(DeathDelay());
    }
    private IEnumerator DeathDelay() {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
    public void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") { //Damages Player
            other.GetComponent<Player>().Damage(2);
            Destroy(gameObject);
        }

        if (other.tag == "Invincible") { //Gets Destroyed when it hits something Invincible (eg. shields or walls)
            Destroy(gameObject);
        }
    }
}