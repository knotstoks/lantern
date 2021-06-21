using System.Collections;
using UnityEngine;

public class Feather : MonoBehaviour {
    [SerializeField] private float lifeTime;
    public Vector3 endPosition;
    private IEnumerator Start() {
        yield return new WaitForSeconds(0.01f);
        transform.right = endPosition - transform.position;
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            other.GetComponent<Player>().Damage(1);
            Destroy(gameObject);
        }
    }
}