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
        if (other.gameObject.tag == "Enemy") {
            other.GetComponent<Enemy>().Damage(10);
        }

        if (other.gameObject.tag == "Boss") {
            other.GetComponent<Boss>().Damage(10);
        }

        if (other.gameObject.tag == "Arm") {
            other.gameObject.GetComponent<Arm>().Damage(10);
        }
    }
}
