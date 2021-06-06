using System.Collections;
using UnityEngine;
public class Supernova : MonoBehaviour {
    [SerializeField] private float lifeTime;
    private void Start() {
        StartCoroutine(Death());
    }
    private IEnumerator Death() {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") {
            other.GetComponent<Enemy>().Damage(10);
        }

        if (other.tag == "Boss") {
            other.GetComponent<Boss>().Damage(10);
        }

        if (other.tag == "Arm") {
            other.GetComponent<Arm>().Damage(10);
        }
    }
}
