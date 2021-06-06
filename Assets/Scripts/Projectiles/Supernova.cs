using System.Collections;
using UnityEngine;
public class Supernova : MonoBehaviour {
    [SerializeField] private float lifeTime;
    [SerializeField] private AudioClip explosion;
    private AudioSource audioSource;
    private void Start() {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(Death());
        audioSource.PlayOneShot(explosion);
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
