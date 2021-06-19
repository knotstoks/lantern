using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feather : MonoBehaviour {
    [SerializeField] private float lifeTime;
    public Vector2 startPosition;
    public Vector2 endPosition;
    private IEnumerator Start() {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
    public void FixAnimation() {
        transform.Rotate(Vector3.forward * (endPosition - startPosition));
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            other.GetComponent<Player>().Damage(1);
            Destroy(gameObject);
        }
    }
}